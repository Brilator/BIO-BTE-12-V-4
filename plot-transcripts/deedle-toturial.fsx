
open FSharp.Data
open Deedle
open Plotly.NET
open Plotly.NET.LayoutObjects

let dataset = 
  Http.RequestString "https://raw.githubusercontent.com/plotly/datasets/master/2014_apple_stock.csv"
   |> fun csv -> Frame.ReadCsvString(csv,true,separators=",")

let getColumn column=
        dataset
        |> Frame.getCol column
        |> Series.values
         
let xy = Seq.zip  (getColumn "AAPL_x" :> seq<DateTime>) (getColumn "AAPL_y" :> seq<float>) 

Series.map


Chart.Line(xy,Name="Share Prices (in USD)")
// |> Chart.withLayout(Layout.init(Title.init("Apple Share Prices over time (2014)"),PlotBGColor=Color.fromString "#e5ecf6",ShowLegend=true,Width=1100))
// |> Chart.withXAxis(LinearAxis.init(Title=Title.init("AAPL_x"),ZeroLineColor=Color.fromString"#ffff",ZeroLineWidth=2.,GridColor=Color.fromString"#ffff" ))
// |> Chart.withYAxis(LinearAxis.init(Title=Title.init("AAPL_y"),ZeroLineColor=Color.fromString"#ffff",ZeroLineWidth=2.,GridColor=Color.fromString"#ffff" ))
|> Chart.show