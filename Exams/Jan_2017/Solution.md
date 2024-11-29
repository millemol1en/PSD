# BSWU BPRD 2017 Jan
## Opgave 1 (25 %): Regulære udtryk og automater
![Alt Text](./SolutionPictures/)

### Opgave Beskrivelse:
"_Betragt dette regulære udtryk over alfabetet `{d, ’,’}`, hvor `d` står for decimalt ciffer og `’,’` er komma. Ved antagelse af, at `d` svarer til tallene fra 0 til 9 og ’,’ er et komma, så beskriver det regulære udtryk kommatal._"

`d+ ','? d∗`

### Question 1:
#### Spørgsmål:
"_Giv en uformel beskrivelse af sproget (mængden af alle strenge) der beskrives af dette regulære udtryk. Giv
mindst 4 eksempler på kommatal der genkendes af dette regulære udtryk og som understøtter din uformelle
beskrivelse._"


#### Svar:


### Question 2:
#### Spørgsmål:
"_Konstruer og tegn en ikke-deterministisk endelig automat (“nondeterministic finite automaton”, NFA) der svarer til det regulære udtryk. Husk at angive starttilstand og accepttilstand(e). Du skal enten bruge en systematisk konstruktion svarende til den i forelæsningen eller som i Introduction to Compiler Design (ICD), eller Basics of Compiler Design (BCD), eller forklare hvorfor den resulterende automat er korrekt._"


#### Svar:


### Question 3:
#### Spørgsmål:
"_Konstruer og tegn en deterministisk endelig automat (“deterministic finite automaton”, DFA) der svarer til det regulære udtryk. Husk at angive starttilstand og accepttilstand(e). Du skal enten bruge en systematisk konstruktion svarende til den i forelæsningen eller som i Introduction to Compiler Design (ICD), eller Basics of Compiler Design (BCD), eller forklare hvorfor den resulterende automat er korrekt._"


#### Svar:


### Question 4:
#### Spørgsmål:
"_Angiv et regulært udtryk, der beskriver kommatal, med følgende egenskaber:_

- _Der tillades kommatal uden komma, dvs. heltal._ 
- _Der tillades maksimalt et komma, og når der er et komma skal der også være mindst et tal foran og efter kommaet._ 
- _Den tomme streng genkendes også af det regulære udtryk._

_Der lægges vægt på at det regulære udtryk ikke umiddelbart kan skrives kortere._"

#### Svar:


## Opgave 2 (20 %): Icon
### Question 1:
#### Spørgsmål:
"_Skriv et Icon udtryk, som udskriver værdierne `1 2 3 4 5 6`_"

#### Svar:
```fsharp
// Code:
let q1 = Every(Write(FromTo(1, 6)))

// Running:
> run q1;;
1 2 3 4 5 6 val it: value = Int 0
```

### Question 2:
#### Spørgsmål:
"_Skriv et Icon udtryk, som udskriver værdierne `33 34 43 44 53 54 63 64`_"

#### Svar:
```fsharp
// Code:
let q2 = Every(Write(Or(FromTo(33, 34), Or(FromTo(43, 44), Or(FromTo(53, 54), FromTo(63, 64))))))

// Running:
> run q2;;
33 34 43 44 53 54 63 64 val it: value = Int 0
```

### Question 3:
#### Spørgsmål:
"_Udvid implementationen af Icon med en ny generator `FromToBy(s,e,i)`, som genererer værdierne mellem `s` og `e` i hop af `i`. Det antages at `s <= e og i >= 0`. FromToBy fejler med det samme, hvis `s > e` eller `i < 0`. Se eksemplet ned forneden:_"

```fsharp
> run (Every(Write(FromToBy(1, 10, 3))));;
1 4 7 10 val it : value = Int 0
```

#### Svar:
```fsharp
type expr = 
  ...
  | FromTo of int * int
  | FromToBy of int * int * int
  ...
  | Fail;;

let rec eval (e : expr) (cont : cont) (econt : econt) = 
    match e with
    ...
    | FromToBy(i1, i2, i3) ->
        let rec loop i =
          if i <= i2 then 
              cont (Int i) (fun () -> loop (i+i3))
          else 
              econt ()
        loop i1
```

### Question 4:
#### Spørgsmål:
"_Skriv en udgave af dit svar til opgave 2 ovenfor, som anvender generatoren `FromToBy`._"

#### Svar:
```fsharp
let ftb = Every(Write(FromToBy(1, 20, 4)))

> run ftb;;
1 5 9 13 17 val it: value = Int 0
```

### Question 5:
#### Spørgsmål:
"_Kan du få konstruktionen `FromToBy` til at generere det samme tal, fx 10, uendelig mange gange? Hvis, ja, så giv et eksempel._"

#### Svar:
```fsharp
let ftbInf = Every(Write(FromToBy(10, 11, 0)))
```


## Opgave 3 (20 %): Print i micro-ML
### Question 1:
#### Opgave Beskrivelse:
"_Udtrykket `print e` skal evaluere `e` til en værdi `v`, som henholdsvis printes på skærmen samt returneres som resultat af udtrykket. Eksempelvis vil udtrykket print 2 udskrive 2 på skærmen og returnere værdien 2._"

#### Spørgsmål:
"_I den abstrakte syntaks repræsenteres funktionen print med `Print e`, hvor `e` er et vilkårligt udtryk. Udvid typen `expr` i `Absyn.fs` med Print således at eksempelvis `Print(CstI 1)` repræsenterer udtrykket der printer konstanten 1 på skærmen og returnerer værdien 1._"

#### Svar:
Changes to file `Absyn.fs`
```fsharp
type expr = 
  ...
  | Print of expr
```

### Question 2:
#### Spørgsmål:
"_Udvid lexer og parser, således at print er understøttet med syntaksen `print e`, hvor print er et nyt nøgleord, se funktionen keyword i filen `FunLex.fsl`._"

#### Svar:
Changes to file `FunLex.fsl`
```text
let keyword s =
    match s with
    ...
    | "print" -> PRINT
```

Changes to file `FunPar.fsy`
```text
%token PRINT

Expr:
   AtExpr                               { $1                     }
   ...
  | PRINT Expr                          { Print($2)              }
```

Examples:
```fsharp
fromString @"print 1"
val it: Absyn.expr = Print (CstI 1)

fromString @"print ((print 1) + 3)"
val it: Absyn.expr = Print (Prim ("+", Print (CstI 1), CstI 3))

fromString @"let f x = x + 1 in print f end"
val it: Absyn.expr = Letfun ("f", "x", Prim ("+", Var "x", CstI 1), Print (Var "f"))
```

### Question 3:
#### Opgave Beskrivelse:
#### Spørgsmål:
"_Udvid funktionen eval i `HigherFun.fs`, med evaluering af `Print e`. Hvis `v` er værdien af at evaluere `e`, så er resultatet af `Print e` at udskrive `v` på skærmen samt returnere `v`._"

"_Til at teste din løsning kan du køre det følgende:_"

```fsharp
let f x = x + 1 in print f end
```

#### Svar:
Changes to `HigherFun.fs`

```fsharp
let rec eval (e : expr) (env : value env) : value =
    match e with
    ...
    | Print e ->
        let eVal = eval e env
        printfn "%A" eVal
        eVal
```

Example execution matches that which they provide in the exam paper:
```fsharp
> run (fromString "let f x = x + 1 in print f end");;
Closure ("f", "x", Prim ("+", Var "x", CstI 1), [])
val it: HigherFun.value = Closure ("f", "x", Prim ("+", Var "x", CstI 1), [])
```

## Opgave 4 (15 %): Pipes i micro-ML:
### Opgave Beskrivelse:
"_Opgaven er at udvide funktionssproget med “pipes”, `>>` og `|>`, kendt fra F#. Hvis `x` er et argument og `f` en funktion, så er operatoren `|>` defineret ved `x |> f = f(x)`, dvs. argumentet `x` “pipes” ind i `f`. Målet er at vi i micro-ML f.eks. kan for neden:_"

```fsharp
let f x = x + 1 in 2 |> f end
```

### Question 1:
#### Spørgsmål:
"_Udvid lexer og parser, således at operatorerne `|>` og `>>` er understøttet. Du kan eksempelvis introducere to nye tokens `PIPERIGHT` for `|>` og `COMPOSERIGHT` for `>>`. Begge operatorer er venstre associative og har en præcedens svarende til lighed (token EQ i filen `FunPar.fsy`). Det betyder bl.a. at `+`, `-`, `*` og `/` alle har højere præcedens end `|>` og `>>`._"


#### Svar:
```fsharp
rule Token = parse
  ...
  | ">>"            { COMPOSERIGHT }
  | "|>"            { PIPERIGHT    }
  ...
  | _               { failwith "Lexer error: illegal symbol" }
  
Expr:
    AtExpr                              { $1                     }
  ...
  | Expr PIPERIGHT Expr                 { Prim("|>", $1, $3)     }
  | Expr COMPOSERIGHT Expr              { Prim(">>", $1, $3)     }
  ...
```


### Question 2:
#### Spørgsmål:
"_Anvend parseren på nedenstående 3 eksempler og forklar den genererede abstrakte syntaks udfra reglerne om præcedens og associativitet af |> og >>. Forklar yderligere, for hvert eksempel, hvorvidt du mener, at den genererede syntaks bør repræsentere et validt micro-ML program_"

```fsharp
// (a)
let f x = x+1 in
    let g x = x+2 in
        2 |> f >> g
    end
end

// (b)
let f x = x+1 in
    let g x = x+2 in
        2 |> (f >> g)
    end
end

// (c)
let f x = x in
    let g x = x in
        2=2 |> (f >> g)
    end
end
```

#### Svar:


```fsharp
// (a)
> fromString @"let f x = x+1 in let g x = x+2 in 2 |> f >> g end end";;
val it: Absyn.expr =
  Letfun
    ("f", "x", Prim ("+", Var "x", CstI 1),
     Letfun
       ("g", "x", Prim ("+", Var "x", CstI 2),
        Prim (">>", Prim ("|>", CstI 2, Var "f"), Var "g")))

// (b)
> fromString @"let f x = x+1 in let g x = x+2 in 2 |> (f >> g) end end";;
val it: Absyn.expr =
  Letfun
    ("f", "x", Prim ("+", Var "x", CstI 1),
     Letfun
       ("g", "x", Prim ("+", Var "x", CstI 2),
        Prim ("|>", CstI 2, Prim (">>", Var "f", Var "g"))))

// (c)
> fromString @"let f x = x in let g x = x in 2=2 |> (f >> g) end end";;
val it: Absyn.expr =
  Letfun
    ("f", "x", Var "x",
     Letfun
       ("g", "x", Var "x",
        Prim ("=", CstI 2, Prim ("|>", CstI 2, Prim (">>", Var "f", Var "g")))))
```

### Question 3:
#### Spørgsmål:
"_Ved brug af de 2 nye regler, angiv et typeinferenstræ for udtrykket for neden:_"


#### Svar:
...


## Opgave 5 (25 %): Tupler i List-C:
### Opgave er ikke mulig:
"_The portion of the assignment has become obsolete_"
