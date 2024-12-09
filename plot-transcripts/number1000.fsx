#r "nuget: Deedle"
#r "nuget: Plotly.NET, 2.0.0"

do fsi.AddPrinter(fun (printer:Deedle.Internal.IFsiFormattable) -> "\n" + (printer.Format()))

open Deedle
open Plotly.NET

let expressionPath = @"plot-transcripts/kallisto_tpmMatrix.csv"

let expressionFrame = Frame.ReadCsv(expressionPath, hasHeaders = true, separators = ",")

let expressionTidy =
    expressionFrame
    |> Frame.indexRowsString "target_id"
    |> Frame.transpose
    |> Frame.mapRowKeys (fun rk ->
        let [|condition; replicate|] = rk.Split('_')
        condition => replicate
    )
    |> Frame.sortRowsByKey
    |> Frame.transpose

let finalPlotData = 
    expressionTidy    
    |> Frame.melt


// warum werden nicht alle elemente geprintet?

expressionTidy.ColumnKeys
|> Seq.map (fun (a,b) -> printfn "%s %s" a b)


