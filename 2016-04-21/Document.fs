namespace FsDocument

type Emphasis = | Regular | Medium | Strong
type TextPart = { Text:string; Emphasis:Emphasis; Style:string option }
                with static member Regular(text) = { Text=text; Emphasis=Regular; Style=None }
                     static member Medium(text) = { Text=text; Emphasis=Medium; Style=None }
                     static member Strong(text) = { Text=text; Emphasis=Strong; Style=None }

type Text = Text of TextPart list
            with static member Regular(text) = Text([TextPart.Regular(text)])
                 static member Medium(text) = Text([TextPart.Medium(text)])
                 static member Strong(text) = Text([TextPart.Strong(text)])
                 static member Block(text) = Text.Regular(text) |> Paragraph
                 static member List(items) = items |> (List.map Text.Regular) |> List
and DocPart =
    | TitledSections of TitledSection list
    | TitledSection of TitledSection
    | Paragraph of Text
    | Image of string
    | List of Text list
    | Table of Style:string option * Rows: RowGroup list
    | Section of Section
and TitledSection = { Title: string; Section: Section }
and RowGroup = | SingleRow of Row | RowGroup of RowGroup list
               with member x.Rows = match x with | SingleRow row -> [row] | RowGroup rgs -> rgs |> List.collect (fun rg -> rg.Rows)
and Row = { Cells: Cell list; Style: string option }
          with static member FromCells(cells) = { Cells = cells; Style = None } |> SingleRow
               static member FromCells(cells, style) = { Cells = cells; Style = Some(style) } |> SingleRow
and Cell = { Parts: DocPart list; RowSpan: int; ColSpan: int; IsHeader: bool }
           with static member New = { Parts = []; RowSpan = 1; ColSpan = 1; IsHeader = false }
                static member Text(text:string) = Cell.Text(Text.Regular(text))
                static member Text(text:Text) = { Parts = [ Paragraph(text) ]; RowSpan = 1; ColSpan = 1; IsHeader = false }
                static member Header(text:string) = Cell.Header(Text.Regular(text))
                static member Header(text:Text) = { Parts = [ Paragraph(text) ]; RowSpan = 1; ColSpan = 1; IsHeader = true }
and Section = { Parts: DocPart list; BreakBefore: bool; Style: string option }
              with static member FromParts(parts) = { Parts = parts; BreakBefore = false; Style = None }
