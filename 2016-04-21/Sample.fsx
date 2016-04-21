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
                        "Titled sections"
                        "Blocks"
                        "Lists"
                    ]
                ] |> Section.FromParts }

        TitledSections [
            {
            Title = "Let's handle emphasis in paragraphs"
            Section =
                [
                    Paragraph(
                        Text [
                            TextPart.Regular "Regular,"
                            TextPart.Medium "Medium"
                            TextPart.Strong "and Strong"
                            TextPart.Regular "in the same paragraph."
                        ])
                ] |> Section.FromParts }
            {
            Title = "Let's handle emphasis in lists"
            Section =
                [
                    List [
                        Text.Regular "Regular"
                        Text.Medium "Medium"
                        Text.Strong "Strong"
                    ]
                ] |> Section.FromParts }
        ]        
    ]