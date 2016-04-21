#load "Document.fs"
open FsDocument

let sample =
    [
        TitledSection {
            Title = "Welcome"
            Section =
                [
                    Text.Block "This is an introduction."
                    Text.Block "Let's try to render simple elements such as:"
                    Text.List [
                        "Blocks"
                        "Lists"
                    ]
                ] |> Section.FromParts }
        
    ]