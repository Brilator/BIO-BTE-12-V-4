#r "nuget: BioFSharp"
#r "nuget: BioFSharp.IO"

// Plotly.NET and BioFSharp are opened here.
open BioFSharp
open BioFSharp.IO
// Additional namespaces/modules are also opened here to simplify the signatures.
open BioArray
open BioSeq
open AminoAcids
open Nucleotides

fsi.AddPrinter(BioFSharp.IO.FSIPrinters.prettyPrintBioCollection)

let proteinFasta = 
    FastA.fromFile BioArray.ofAminoAcidString "fasta-spielerei/Athaliana_447_Araport11.protein_primaryTranscriptOnly.fa"

proteinFasta
|> Seq.item 11
|> fun fastaItem -> fastaItem.Sequence


let subSeq i n = Seq.skip i >> Seq.take n

proteinFasta
|> subSeq 2 10
|> Seq.length


proteinFasta
|> subSeq 1 5
|> Seq.map (fun fastaItem -> fastaItem.Sequence)
|> FastA.write BioItem.symbol "test.fasta"