
#r "nuget: Deedle, 3.0.0-beta.1"
#r "nuget: Deedle.Interactive, 3.0.0-beta.1"
open Deedle

let coffeesPerWeek  = Series.ofValues [15;12;10;11] 

let firstNames      = Series.ofValues ["Kevin";"Lukas";"Benedikt";"Michael"]

let lastNames       = Series.ofValues ["Schneider";"Weil";"Venn";"Schroda"]  
let group           = Series.ofValues ["CSB";"CSB";"CSB";"MBS"] 


let persons = Frame.ofColumns ["first", firstNames; "last", lastNames; "g", group]

persons


// Add a newly created series named `"sodasPerWeek"` and the given series `"coffeesPerWeek"` as columns to the frame. Bind the resulting frame to a new name.
// Tip: Create a `Series<int,int>` first. Use `Frame.addCol`


let sodasPerWeek = Series.ofValues [9;12;4;2]

let drinksFrame =
    persons
    |> Frame.addCol "spw" sodasPerWeek
    |> Frame.addCol "cpw" coffeesPerWeek

drinksFrame

// ## Task 1.4
// Add the columns `"sodasPerWeek"` and `"coffeesPerWeek"`. Add the resulting series as a column with the title `"allPurchases"` to the previously created frame.
// Tip 1: This task can be solved in several ways.
// Tip 2: Via `Series.values` you can access the values of each Series. Then you could iterate over both collections with `Seq.map2`. 


// allPurchases

let GroupedG :Frame<string*int,_> = 
    drinksFrame
    |> Frame.groupRowsBy "g"

// let allPurchases :Series<string*int, int> = 
//         GroupedG |> Frame.getCol "spw" 


let allPurchases :Frame<string*int,_>  =        
    GroupedG
    |> Frame.sliceCols ["spw";"cpw"]






Frame.sliceCols ["spw";"cpw"] GroupedG



Frame.getCol "cpw" drinksFrame





let x6 = 
    let n=1 in n+2

let x4 = for i in [1..10]
            do printf "%i" i


let test b t f = if b then t else f

let f = test true (lazy (printfn "true")) (lazy (printfn "false"))

f.Force()


let xy = seq {
    // "yield" adds one element
    1
    2

    // "yield!" adds a whole subsequence
    yield! [5..500]
}



List.init 5 (fun i -> 2 * i + 1)


let x i = (fun i -> 2 * i + 1)

x 2



let list1 = [ 1; 2; 3 ; 5 ]
let list2 = [ -1; -2; -3 ; -4]
let listZip = List.zip list1 list2
printfn "%A" listZip



let list1 = [1; 2; 3]
let list2 = [4; 5; 6]
List.iter (fun x -> printfn "List.iter: element is %d" x) list1
List.iteri(fun i x -> printfn "List.iteri: element %d is %d" i x) list1
List.iter2 (fun x y -> printfn "List.iter2: elements are %d %d" x y) list1 list2
List.iteri2 (fun i x y ->
                printfn "List.iteri2: element %d of list1 is %d element %d of list2 is %d"
                  i x i y)
            list1 list2


let list1 = [1 .. 43]
let newList = 
    list1
    |> List.map (fun x -> x * 13)
    |> List.filter (fun y -> y % 2 = 0)

printfn "%A" newList




let someList = [2 .. 20]

someList


let greetings =
    if (System.DateTime.Now.Hour < 12)
    then (fun name -> "good morning, " + name)
    else (fun name -> "good day, " + name)

//test
greetings "Alice"


let sum list = List.reduce (+) list

sum someList


List.reduce (+) [1 .. 12]

failwithf "something"


let x =
    match 1 with
    | _ -> "z"
    | 1 -> "a"
    | 2 -> "b"

x



module Squarer =

    let square input =
        let result = input * input
        result

    let printSquare input =
        let result = square input
        printfn "Input=%i. Result=%i" input result


Squarer.square 2