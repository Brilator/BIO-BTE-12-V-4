#r "nuget: Deedle"
#r "nuget: Plotly.NET, 2.0.0"
#r "nuget: Plotly.NET.Interactive, 2.0.0"
open Plotly.NET
open Plotly.NET.LayoutObjects
open Plotly.NET.TraceObjects
open Deedle

do fsi.AddPrinter(fun (printer:Deedle.Internal.IFsiFormattable) -> "\n" + (printer.Format()))

let wikiStromerzeugung : Frame<string,string> = 
    Frame.ReadCsv("notebooks/english/07-Task2-2.csv", hasHeaders = true, separators = ",")
    |> Frame.indexRows "Energietraeger"


wikiStromerzeugung.ColumnKeys

wikiStromerzeugung
|> Frame.getRows
|> Series.map (fun rk values -> 
    let values = 
        values.Observations
        |> Seq.choose(fun kv -> 
            if kv.Value = "k. A." then
                None
            else 
                Some(kv.Key, kv.Value)
        )
    Chart.Line(values, Name = rk)
)

let tryParseFloat (s:string) = 
    let b,v = System.Double.TryParse(s.Replace(",", "."))
    if b then
        Some(v)
    else
        None


let chartFromWikis (key:string) (data:Series<string,string>) = 
    let values = 
        data.Observations
        |> Seq.choose(fun kv -> 
            match tryParseFloat kv.Value with 
            | Some(v) -> Some(kv.Key, v)
            | None -> None
        )
    Chart.Line(values, Name = key)
    
    
let chooseFloats (data:seq<string*string>) = 
    data
    |> Seq.choose(fun (k, v) -> 
        match tryParseFloat v with 
        | Some(v) -> Some(k, v)
        | None -> None
    )



wikiStromerzeugung.GetRow<string> ("Braunkohle")
|> chartFromWikis "Braunkohle"
|> Chart.withTitle "Braunkohle"
|> Chart.show


wikiStromerzeugung.RowKeys
|> Seq.map (fun rk ->
    wikiStromerzeugung.GetRow<string> (rk)
    |> Series.observations
    |> chooseFloats
    |> fun vs -> Chart.Line (vs, Name = rk)
)
// |> Chart.combine
|> Chart.Grid (4,4)
|> Chart.withTitle "Strom"
|> Chart.show

