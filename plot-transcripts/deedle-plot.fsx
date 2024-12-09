#r "nuget: Deedle"
#r "nuget: Plotly.NET, 2.0.0"

do fsi.AddPrinter(fun (printer:Deedle.Internal.IFsiFormattable) -> "\n" + (printer.Format()))

open Deedle
open Plotly.NET

let expressionPath = @"plot-transcripts/kallisto_tpmMatrix.csv"
let familyPath = @"plot-transcripts/gene-cats.csv"

let expressionFrame : Frame<string,string> = 
    Frame.ReadCsv(expressionPath, separators = ",")
    |> Frame.indexRows "target_id"

let familyFrame : Frame<string,string> = 
    Frame.ReadCsv(familyPath, separators = "\t", hasHeaders = false)
    |> Frame.indexRows "Column1"


let CAM = 
    expressionFrame
    |> Frame.sliceCols ["CAM_01"; "CAM_02"; "CAM_03"]
    |> Frame.fillMissingWith 0.

let camMelt = Frame.melt CAM


let getColumn column dataframe=
    dataframe
    |> Frame.getCol column
    |> Series.values


let xy = Seq.zip  (getColumn "Row" camMelt :> seq<string>) (getColumn "Value" camMelt :> seq<float>) 

Chart.BoxPlot(xy)
|> Chart.show




let C3 = 
    expressionFrame
    |> Frame.sliceCols ["reC3_01"; "reC3_02"; "reC3_03"]
    |> Frame.fillMissingWith 0.
    // |> Frame.melt


let combinedFrame : Frame<string*string,string> = 
    Frame.join JoinKind.Inner C3 familyFrame
    |> Frame.groupRowsBy "Column2"


combinedFrame
|> Frame.nest
// |> Series.getAt 0
|> Series.map (fun (geneName:string) (f : Frame<string,string>) ->
    [
    f.GetColumn<float> "reC3_01" |> Series.values |> fun v -> Chart.BoxPlot (y = v, Name = "CAM 01")
    f.GetColumn<float> "reC3_02" |> Series.values |> fun v -> Chart.BoxPlot (y = v, Name = "CAM 02")
    f.GetColumn<float> "CAM_03" |> Series.values |> fun v -> Chart.BoxPlot (y = v, Name = "CAM 03")
    ]
    |> Chart.combine
    |> Chart.withTitle geneName
    |> Chart.show
)


combinedFrame
|> Frame.nest
|> Frame.GetColumn<float> "CAM_01" |> Series.values
