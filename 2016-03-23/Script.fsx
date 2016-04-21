type Jeu = int * int
type Set = Jeu list
type Score = Set
type Player = PlayerA | PlayerB
type Log = Player list

let displayJeu jeu =
    match jeu with
    | 0, 0 -> "0 - 0"
    | 1, 0 -> "15 - 0"
    | 2, 0 -> "30 - 0"
    | 3, 0 -> "40 - 0"
    | 0, 1 -> "0 - 15"
    | 1, 1 -> "15 A"
    | 2, 1 -> "30 - 15"
    | 3, 1 -> "40 - 15"
    | 0, 2 -> "0 - 30"
    | 1, 2 -> "15 - 30"
    | 2, 2 -> "30 A"
    | 3, 2 -> "40 - 30"
    | 0, 3 -> "0 - 40"
    | 1, 3 -> "15 - 40"
    | 2, 3 -> "30 - 40"
    | a, b when a = b -> "Deuce"
    | a, b when a > b -> "Avantage A"
    | _ -> "Avantage B"

let display (score:Score) =
    match score with
    | [] -> ""
    | [jeu] -> displayJeu jeu
    | jeu :: jeux ->
        let a, b = jeux |> Seq.fold (fun (nbJeuxA, nbJeuxB) (pointsA, pointsB) -> if pointsA > pointsB then (nbJeuxA + 1, nbJeuxB) else (nbJeuxA, nbJeuxB + 1)) (0,0)
        sprintf "%d : %d|%s" a b (displayJeu jeu)

let apply (score:Score) player =
    let jeu, jeux =
        match score with
        | [] -> (0, 0), []
        | jeu :: jeux -> jeu, jeux
    
    let nouveauJeu =
        let a, b = jeu
        match player with
        | PlayerA -> a + 1, b
        | PlayerB -> a, b + 1

    let jeuGagnant =
        match nouveauJeu with
        | a, b when a > 3 && a > b + 1 -> true
        | a, b when b > 3 && b > a + 1 -> true
        | _ -> false

    if jeuGagnant then
        (0,0) :: nouveauJeu :: jeux
    else
        nouveauJeu :: jeux

let replay log = 
    log |> Seq.fold apply []

let shouldEqual x y =
    if x <> y then failwithf "expected %A <> actual %A" x y

let test() =
    try
        let score = replay []
        display score |> shouldEqual ""

        let score = replay [PlayerA]
        score |> shouldEqual [(1,0)]
        display score |> shouldEqual "15 - 0"

        let score = replay [PlayerA;PlayerB;PlayerA;PlayerA]
        score |> shouldEqual [(3,1)]
        display score |> shouldEqual "40 - 15"

        let score = replay [PlayerA;PlayerB;PlayerA;PlayerA;PlayerA]
        score |> shouldEqual [(0,0);(4,1)]
        display score |> shouldEqual "1 : 0|0 - 0"

        let score = replay [PlayerA;PlayerB;PlayerA;PlayerA;PlayerA;PlayerB;PlayerB;PlayerB]
        score |> shouldEqual [(0,3);(4,1)]
        display score |> shouldEqual "1 : 0|0 - 40"

        printfn "%s" "All tests OK - You're GOOOOOOOOOD!"
    with e ->
        printfn "*********** %s  ************" e.Message

