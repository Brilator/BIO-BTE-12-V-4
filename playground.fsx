#r "nuget: FSharp.Data"

#r "nuget: FSharp.Stats, 0.4.5"
#r "nuget: Plotly.NET, 2.0.0-preview.18"
#r "nuget: Plotly.NET.Interactive, 2.0.0-preview.18"

open FSharp.Stats
open Plotly.NET
open Plotly.NET.LayoutObjects
open Plotly.NET.TraceObjects

open Plotly.NET
open FSharp.Data
open Plotly.NET.TraceObjects
open Plotly.NET.LayoutObjects

let data = CsvFile.Load("https://raw.githubusercontent.com/plotly/datasets/master/us-cities-top-1k.csv")

let getData state = data.Rows 
                        |> Seq.filter (fun row -> row.GetColumn("State") = state)
                        |> Seq.map (fun row -> row.GetColumn("lon"),row.GetColumn("lat"))

let newyorkData = getData "New York"
let ohioData = getData "Ohio"

[
    Chart.LineMapbox(newyorkData,Name="New York")
    Chart.LineMapbox(ohioData,Name="Ohio")
]
|> Chart.combine
|> Chart.withMapbox(mapBox = Mapbox.init(Style=StyleParam.MapboxStyle.StamenTerrain,Center=(-80.,41.),Zoom=3.))
|> Chart.withMarginSize(Left=0,Right=0,Top=0,Bottom=0)




Seq.meanBy [3.1;3.2;3.3;4.5]


open FSharp.Stats.Distributions

let distA = Continuous.normal 13.0 1.5
let distB = Continuous.normal 15.0 1.5

distA.Sample()


distA.PDF 13
distA.CDF 13