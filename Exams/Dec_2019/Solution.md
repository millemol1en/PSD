# BSWU BPRD 2019 Dec
## Opgave 1 (20%): Regulære udtryk og automater
![Alt Text](./SolutionPictures/Exam%202019%20Question%201%20NFA.png)

### Question 1:
#### Spørgsmål:
"_Angiv alle årsager til at automaten er ikke–deterministisk (NFA)._"

#### Svar:
- Node `1` has 2 out-going edges with `a` as the state change.
- Node `2` has 2 out-going edges with `b` as the state change.
- There is an epsilon from node `3` to node `4`.

### Question 2:
#### Spørgsmål:
"_Giv tre eksempler på strenge der genkendes af automaten._"


#### Svar:
 - "ab"  `[1 -> 3 -> 4 -> 5]`
 - "abb" `[1 -> 2 -> 4 -> 5]`
 - "aaaaaaaab" `[1 -> 3 -> 4 -> repeat -> 5]`

### Question 3:
#### Spørgsmål:
"_Giv en uformel beskrivelse af sproget (mængden af alle strenge) der beskrives af automaten._"

#### Svar:
The NFA accepts the languages of all words {a, b} where there is at least 1 `a` in the beginning and 1 `b` at the end. 


### Question 4:
#### Spørgsmål:
"_Konstruer og tegn en deterministisk endelig automat (“deterministic finite automaton”, DFA) der svarer til automaten ovenfor. Husk at angive starttilstand og accepttilstand(e). Du skal enten bruge en systematisk konstruktion svarende til den i forelæsningen eller som i Introduction to Compiler Design (ICD), eller Basics of Compiler Design (BCD), eller forklare hvorfor den resulterende automat er korrekt._"


#### Svar:
##### Step 1: Remove the Epsilon:
The easiest is to first remove the epsilon from our NFA. To do this, we create a table with the epsilon present so we can identify which states we can stitch together. 

|   | a     | b     | ε     |
|---|-------|-------|-------|
| 1 | {2,3} | Ø     | 1     |
| 2 | Ø     | {3,4} | 2     |
| 3 | Ø     | Ø     | {3,4} |
| 4 | 4     | 5     | 4     |
| 5 | Ø     | Ø     | 5     |

**REMEMBER!** An epsilon will always contain the node itself - hence the reason why node `1` has node `1`.

During this step, we are ONLY focused on the result of the epsilon connection. Our goal is to remove them by stitching existing nodes together. As node `3` references nodes `{3,4}`, we can conclude that these need to be stitched together - giving us `α`, a combination of nodes `3` and `4`:

|               | a     | b     | ε     |
|---------------|-------|-------|-------|
| 1             | {2,3} | Ø     | 1     |
| 2             | Ø     | {3,4} | 2     |
| **α** {3,4}   | 4     | 5     | {3,4} | 
| 5             | Ø     | Ø     | 5     |

![Alt Text](./SolutionPictures/Exam%202019%20Question%201%20NFA%20No%20Epsilon.png)


##### Step 2: Convert NFA to DFA:
Using the slimmed down NFA, we can now create the table to 

|                          | a                        | b                    |
|--------------------------|--------------------------|----------------------|
| 1     := (S<sub>0</sub>) | {2,α} := (S<sub>1</sub>) | Ø                    |
| 2     := N/A             | Ø                        | α := (S<sub>2</sub>) |
| α     := (S<sub>3</sub>) | α := (S<sub>2</sub>)     | 5 := (S<sub>4</sub>) | 
| 5     := (S<sub>4</sub>) | Ø                        | Ø                    |
| {2,α} := (S<sub>1</sub>) | α := (S<sub>2</sub>)     | α := (S<sub>3</sub>) |
| {α,5} := (S<sub>3</sub>) | α := (S<sub>2</sub>)     | 5 := (S<sub>4</sub>) |

![Alt Text](./SolutionPictures/Exam%202019%20DFA.png)

### Question 5:
#### Spørgsmål:
"_Angiv et regulært udtryk der beskriver mængden af strenge over alfabetet {a, b}, som beskrives af automaten ovenfor. Check og forklar at det regulære udtryk også beskriver mængden af strenge for din DFA._"


## Opgave 2 (25%): Micro–ML: Intervalcheck
### Question 1:
#### Spørgsmål:
"_Opgaven er at udvide funktionssproget med intervalcheck, som er udtryk af formen e within `[e1,e2]` der returnerer sand `(1)` eller falsk `(0)` afhængig af om `v1 ≤ v ≤ v2` er opfyldt, hvor `v`, `v1` og `v2` er resultaterne af at evaluere `e`, `e1` og `e2`. I den abstrakte syntaks repræsenteres et intervalcheck med `InCheck (e, e1, e2)`, hvor `e`, `e1` og `e2` er vilkårlige
udtryk._"

```fsharp
let x = 23 in x within [2+3,40] end
```

### Question 2:
#### Spørgsmål:
"_Udvid lexer FunLex.fsl og parser FunPar.fsy således at du kan parse intervalcheck, eksempelvis_"

##### Changes to`FunLex.fsl`:
```c++
let keyword s =
    match s with
    ...
    | "within" -> WITHIN         // Target change
    ...
    | _        -> NAME s
}
```


```c++
rule Token = parse
  ...
  | '['             { LSQRPAR }
  | ']'             { RSQRPAR }
  | ','             { COMMA }
  ...
  | _               { failwith "Lexer error: illegal symbol" }
```

##### Changes to`FunPar.fsy`:


```fsharp
fromString @"let x = 23 in x within [2+3,40] end";;
->
Let ("x", CstI 23, InCheck (Var "x", Prim ("+", CstI 2, CstI 3), CstI 40))
```

### Question 3:
```fsharp
let rec eval (e : expr) (env : value env) : value =
    match e with
    ...
    | InCheck (e, e1, e2) ->
      // Evaluate the expression 'e', 'e1' and 'e2'
      let eVal  = eval e env
      let e1Val = eval e1 env
      let e2Val = eval e2 env
      
      // Determine whether 'e1 <= e' and 'e <= e2'
      let e1Bool = if e1Val <= eVal then 1 else 0
      let e2Bool = if eVal <= e2Val then 1 else 0
      
      Int (if e1Bool = e2Bool then 1 else 0)
    ...

```


### Question 4:


## Opgave 3 (25%): Micro–C: Print Variable Environment
### Opgave Beskrivelse:
"_I denne opgave udvider vi oversætteren Comp.fs til under oversættelse at kunne udskrive indholdet af variabel environment varEnv. Målet er at lave en hjælpefunktion `ppVarEnv`, af type `varEnv → string
list`, som kan anvendes de steder i oversætteren Comp.fs, hvor man ønsker environment udskrevet, eksempelvis for at debugge et problem. For lokale variable returneres en streng formatteret efter følgende skabelon:
\n n:t at `bp[o]`, hvor n er variabelnavn, t er variablens type og o er offset i forhold til base pointer bp
i stack frame. For globale variable anvendes samme skabelon `\n n:t at Global[o]`, hvor `Global[o]`
angiver den globale variabels absolutte adresse `o` `i` bunden af stakken._"

"_Nedenfor ses et eksempel, som vi antager ligger i filen exam1.c._"
```c++
// micro-C December 2019 example 1
int g;
void main() {
    int n;
    int *p;
    int *pn[1];
    int a[2];
    // exVarEnv svarer til varEnv her.
}
```

### Question 1:
#### Spørgsmål:
"_Værdien exVarEnv af type varEnv i filen `Comp.fs` er et par `(ve, o)`, hvor anden komponenten `o` har værdien 7. Forklar hvad `o` anvendes til og hvordan værdien 7 passer med værdien af første komponenten `[("a", (Locvar 6, TypA (TypI,Some 2))); . . .]`._"

```fsharp
let exVarEnv : varEnv =
    ([("a", (Locvar 6, TypA (TypI,Some 2)));
    ("pn", (Locvar 3, TypA (TypP TypI,Some 1)));
    ("p", (Locvar 1, TypP TypI));
    ("n", (Locvar 0, TypI));
    ("g", (Glovar 0, TypI))], 7)
```

#### Svar:
Anden komponenten `o` er offset til næste lokale variabel i stack frame. Det bliver beskrevet i type beskrivelsen i filen `Comp.fs`. Det passer så med første komponenten, hvor den seneste lokale variabel a har fået offset 6.

### Question 2:
#### Spørgsmål:
"_For hver variabel skal vi formattere dens type. I filen Absyn.fs er en type defineret som_"
```fsharp
type typ =
| TypI                      (* Type int     *)
| TypC                      (* Type char    *)
| TypA of typ * int option  (* Array type   *)
| TypP of typ               (* Pointer type *)
```


#### Svar:
```fsharp
// Implementation
let rec ppTyp (t : typ) : string = 
  match t with
  | TypI -> "int"
  | TypC -> "char"
  | TypP pT -> "(*" + (ppTyp pT ) + ")"         // "pT" for pointer type
  | TypA (aT, idx) ->                           // "aT" for array type
    let idxVal =                                
      match idx with
      | None   -> ""
      | Some n -> string n
    
    "(" + (ppTyp aT) + "[" + idxVal + "])"


// Eksempler:
```

### Question 3:
#### Spørgsmål:
"_Implementer, i filen `Comp.fs`, en funktion ppVar v af type var -> string, der returnerer en streng repræsentation af variablen’s adresse på stakken. For globale variable er det en **fast adresse** og for lokale variable et **offset** i forhold til **base pointer**. Nedenstående eksempler viser hvorledes formatteringen ønskes._"

#### Svar:
Implementationen og eksempler ses ned for neden:
```fsharp
// Implementation:
let ppVar (variable : var) : string = 
    match variable with
    | Glovar iVal -> "Global[" + string iVal + "]"
    | Locvar iVal -> "bp[" + string iVal + "]"
    
    
// Eksempler:
> ppVar (Glovar 1);;
val it: string = "Global[1]"

>  ppVar (Locvar 3);;
val it: string = "bp[3]"
```

### Question 4:
#### Spørgsmål:
"_Implementer, i filen Comp.fs, en funktion `ppVarTyp (s,(v,t))` af type `string*(var*typ) →
string` som formatterer variablen s, dens adresse v og type t efter skabelonerne ovenfor. Nedenstående
eksempler viser hvorledes formatteringen kan se ud afhængig af din valgte formattering i `ppTyp`._"

#### Svar:
Implementationen og eksempler ses ned for neden:

```fsharp
// Implementation:
let ppVarTyp ((s, (v, t)) : string * (var * typ)) : string =
    let ppVarRes = ppVar v      // local or global
    let ppTypRes = ppTyp t      // variable type
    
    s + ":" + ppTypRes + " at " + ppVarRes

// Eksempler:
> open Absyn;;
> open Comp;;

> ppVarTyp ("a",(Locvar 11, TypA (TypI, Some 3)));;
val it: string = "a:(int[3]) at bp[11]"

> ppVarTyp ("g",(Glovar 0, TypI));;
val it: string = "g:int at Global[0]"
```


### Question 5:
#### Spørgsmål:
"_Implementer funktionen `ppVarEnv` af type `varEnv → string list`, som anvender skabelonerne for
lokale og globale variable ovenfor. Så funktionen `ppVarTyp`_"


#### Svar:
```fsharp
// Implementation:
let ppVarEnv ((vEnv, _) : varEnv) : string list =
    let rec aux (env : (var * typ) env) acc : string list =
        match env with
        | []    -> acc
        | x::xs -> aux xs ((ppVarTyp x) :: acc)
    
    aux vEnv []


// Eksempel:
> open Absyn;;
> open Comp;;
> let exVarEnv : varEnv =
-   ([("a", (Locvar 6, TypA (TypI,Some 2)));
-   ("pn", (Locvar 3, TypA (TypP TypI,Some 1)));
-   ("p", (Locvar 1, TypP TypI));
-   ("n", (Locvar 0, TypI));
-   ("g", (Glovar 0, TypI))], 7);;

val exVarEnv: varEnv =
  ([("a", (Locvar 6, TypA (TypI, Some 2)));
    ("pn", (Locvar 3, TypA (TypP TypI, Some 1))); ("p", (Locvar 1, TypP TypI));
    ("n", (Locvar 0, TypI)); ("g", (Glovar 0, TypI))], 7)

val it: string list =
  ["g:int at Global[0]"; "n:int at bp[0]"; "p:(*int) at bp[1]";
   "pn:((*int)[1]) at bp[3]"; "a:(int[2]) at bp[6]"]
```

### Question 6:
#### Spørgsmål:
"_Vi ønsker nu at oversætteren skal udskrive variabel environment på skærmen hver gang den når til sidste
statement eller declaration i en blok. Så opgaven går du på at finde det sted i funktionen `loop` skal have indsat et funktion kald til `printVarEnv`._"



```fsharp
//
let rec cStmt stmt (varEnv : varEnv) (funEnv : funEnv) : instr list =
    match stmt with
    | If(e, stmt1, stmt2) ->
        ...
    | Block stmts ->
        let rec loop stmts varEnv =
            match stmts with
            | [] -> (snd varEnv, [])
            | s1::sr ->
            let (varEnv1, code1) = cStmtOrDec s1 varEnv funEnv
            let (fdepthr, coder) = loop sr varEnv1
            (fdepthr, code1 @ coder)
        
        let (fdepthend, code) = loop stmts varEnv
        code @ [INCSP(snd varEnv - fdepthend)]
    | Return None -> ...

// Funktionen som vi skal kalde på:
let printVarEnv varEnv =
    List.iter (printf "\n%s") (ppVarEnv varEnv);
    printf "\n"
```

#### Svar:
Vi indsætter funktion kaldet til at printe lige før vi er færdig med at løbe igennem vores `stmts` block. 
```fsharp
let rec cStmt stmt (varEnv : varEnv) (funEnv : funEnv) : instr list =
    match stmt with
    | If(e, stmt1, stmt2) ->
        ...
    | Block stmts ->
        let rec loop stmts varEnv =
            match stmts with
            | []     ->
                        printVarEnv varEnv      // FUNCTION CALL HERE!
                        (snd varEnv, [])
            | s1::sr ->
            let (varEnv1, code1) = cStmtOrDec s1 varEnv funEnv
            let (fdepthr, coder) = loop sr varEnv1
            (fdepthr, code1 @ coder)
        
        let (fdepthend, code) = loop stmts varEnv
        code @ [INCSP(snd varEnv - fdepthend)]
    | Return None -> ...
```


Det får vores program til at printe det følgende når vi kører det:
```text
> open ParseAndComp;;
> compile "exam1";;

g:int at Global[0]
n:int at bp[0]
p:(*int) at bp[1]
pn:((*int)[1]) at bp[3]
a:(int[2]) at bp[6]

val it: Machine.instr list =
  [INCSP 1; LDARGS; CALL (0, "L1"); STOP; Label "L1"; INCSP 1; INCSP 1;
   INCSP 1; GETSP; CSTI 0; SUB; INCSP 2; GETSP; CSTI 1; SUB; INCSP -7; RET -1]
```

## Opgave 4 (30%): Micro–ML: Print Current Stack Frame
#### Opgave Beskrivelse:
"_I denne opgave tilføjer vi et nyt statement printCurFrame, som udskriver alle lokale variable i scope på
det givne sted samt deres indhold på skærmen. Dette er en fortsættelse af opgave 3, men kræver ikke at opgave 3
er løst._"

"_Målet med opgaven er at få uddata svarende til nedenstående, når programmet afvikles med Java bytekode
maskinen:_"

```text
$ java Machine exam2.out
Current Stack Frame (bp=3):
    n at bp[0] = 1234
    p at bp[1] = 3
    pn at bp[3] = 5
    a at bp[6] = 7
Ran 0.019 seconds
```

### Question 1:
#### Spørgsmål & Svar:
"_Beskriv indholdet af stakken ved instruktion `PRINTCURFRM` (programadresse 89) ved at udfylde nedenstående skema. Du kan antage at base pointer bp er lig 3._"

| Stack Addr | Value | Description                                                                                                                                         |
|------------|-------|-----------------------------------------------------------------------------------------------------------------------------------------------------|
| 0          | 42    | Global variable `g` is set to 42 `Global[0]=42`.                                                                                                    |
| 1          | 6     | Return address from main                                                                                                                            |
| 2          | -999  | Old base pointer - it doesn't exist so it gets set to `-999`                                                                                        |
| 3          | 1234  | Local variable `n` with a value of `1234`                                                                                                           |
| 4          | 3     | Local variable `*p` which a pointer pointing at address `3`                                                                                         |
| 5          | 0     | Local variable `*pn[1]`, which is an array of pointers with length 1, has its first value at index position `[0]` set to the address of g, so `&g`. |
| 6          | 5     | _...continuation of stuff from above..._                                                                                                            |
| 7          | 2     | Local variable `a[0]`, which is an array of values with length 2, has `a[0]` set to 2...                                                            |
| 8          | 4     | ... and then `a[1]` is set to `4`.                                                                                                                  |
| 9          | 7     | The value here references the value at index 0 for the array `a[0]`.                                                                                |

### Question 2:
#### Spørgsmål:
"_Du skal udvide lexer `CLex.fsl`, parser `CPar.fsy` og `Absyn.fs` med support for `printCurFrame;`._"

#### Svar:
Take note that the decision to make `printCurFrame` a `stmt` was based on the fact that the exam provided an example print of `exam2.c` wherein `PrintCurFrame` is proceeded by a `Stmt`:

```fsharp
> open ParseAndComp;;
> fromFile "./Exercises/exam2.c";;

Block
    [...
    Stmt (Expr (Assign (AccIndex (AccVar "a",CstI 1),CstI 4)));
    Stmt PrintCurFrame])]   // This here!
```

##### File `CLex.fsl` 

```fsharp
let keyword s =
    match s with
    ...
    | "println"         -> PRINTLN
    | "printCurFrame"   -> PRINTCURFRAME
    ...
    | _         -> NAME s
```

##### File `CPar.fsy`
We add it as a token:
```fsharp
%token PRINTCURFRAME
```

We add it as a `Stmt` and don't forget the `SEMI` like I did! Remember that we literally need to build to character-by-character:
```fsharp
StmtM:  // No unbalanced if-else
    Expr SEMI                           { Expr($1)             }
  | RETURN SEMI                         { Return None          }
  ...
  | PRINTCURFRAME SEMI                  { PrintCurFrame        }
  ...
```

```fsharp
and stmt =                                                         
  ...
  | Switch of expr * (int * stmt) list (* Question 8.6 *)
  | PrintCurFrame 
```

The result should be idenitical to the print provided in the exam.

### Question 3:
#### Spørgsmål:
"_For at udskrive en stack frame på køretid, implementerer vi en ny bytekode instruktion `PRINTCURFRM` i `Machine.fs`. Instruktionen PRINTCURFRM optager `2 + SUM (2 + vi.length)` ord, hvor n er antallet af variable og `vi.length` er længden af variabelnavn `vi`. Ændringer skal laves til funktionerne `makelabenv` og `emitints`._"

#### Svar:
##### Function `makelabenv`
```fsharp
let makelabenv (addr, labenv) instr = 
    match instr with
    | Label lab       -> (addr, (lab, addr) :: labenv)
    ...
    | PRINTCURFRM env ->
        let calcLength (_, s : string) = 2 + s.Length                           //
        (addr + (List.fold (fun acc e -> acc + calcLength e) 2 env), labenv)    //
```

##### Function `emitints`
```fsharp
let rec emitints getlab instr ints = 
    match instr with
    ...
    | PRINTCURFRM env ->
        let codeString (s:string) = s.Length :: [for c in s -> (int) c] //
        let codeVar (i,s) C = i :: (codeString s @ C)                   //
        CODEPRINTCURFRM ::                                              //
        List.length env ::                                              //
        (List.foldBack codeVar env ints)                                //
```

The `emitints` ...

### Question 4:
#### Spørgsmål:
"_Bytekode maskinen Machine.java skal udvides med PRINTCURFRM. Du kan anvende følgende skabelon for at implementere PRINTCURFRM i funktionen execcode i Machine.java. Funktionen kan findes i eksamen:_"

"_Du får også den følgende funktion som udskriver vores stack:_"

```csharp
static void printStr(int[] p, int pc) {
    for (int i=0; i<p[pc]; i++)
        System.out.print((char)(p[pc+1+i]));
}
```

#### Svar:
```csharp
final static int 
    ...
    PRINTCURFRM = 26;
  
static int execcode(int[] p, int[] s, int[] iargs, boolean trace) {
  int bp = -999;	// Base pointer, for local variable access 
  int sp = -1;	    // Stack top pointer
  int pc = 0;		// Program counter: next instruction
  for (;;) {
    if (trace) 
      printsppc(s, bp, sp, p, pc);
    switch (p[pc++]) {
        ... 
    // Newly added code
    case PRINTCURFRM:
        System.out.print("Current Stack Frame (bp=" + bp + "):\n");
        int N = p[pc++];
        for (int i=0; i<N;i++) {
          int o = p[pc++];
          System.out.print(" ");
          printStr(p,pc); // Print variable v_i.
          pc += p[pc] + 1;
          System.out.print(" at bp[" + o + "] = "+s[bp+o]);
          System.out.print("\n");
        }
        break;
        
        ...
    }
```


### Question 5
#### Spørgsmål:
"For at implementere oversættelsen af PrintCurFrame i Comp.fs kan du anvende nedenstående kode:"
```fsharp
let isLocvar = function
    | Glovar _ -> false
    | Locvar _ -> true
let getOffset = function
    | Glovar i -> i
    | Locvar i -> i

let rec cStmt stmt (varEnv : varEnv) (funEnv : funEnv) : instr list =
    match stmt with
    | ...
    | PrintCurFrame ->
        let varEnv' = List.filter (fun (_,(v,_)) -> isLocvar v) (fst varEnv)
        [PRINTCURFRM (List.map (fun (s,(v,_)) -> ...)
        (List.rev varEnv'))]
    | ...
```

"_Målet er så at implementere det så at du kan oversætte og køre programmet `exam2.c`._"

#### Svar:
```fsharp
// Implementation i "Comp.fs"
| PrintCurFrm ->
        let varEnv' = List.filter (fun (_,(v,_)) -> isLocvar v) (fst varEnv)
        [PRINTCURFRM (List.map (fun (s,(v,_)) -> ((getOffset v), s)) (List.rev varEnv'))]


// Running an example:
> open ParseAndComp;;
> compileToFile (fromFile "./Exercises/exam2.c") "./Exercises/exam2.out"

[INCSP 1; LDARGS; CALL (0, "L1"); STOP; Label "L1"; CSTI 0; CSTI 42; STI;
   INCSP -1; INCSP 1; GETBP; CSTI 0; ADD; CSTI 1234; STI; INCSP -1; INCSP 1;
   GETBP; CSTI 1; ADD; GETBP; CSTI 0; ADD; STI; INCSP -1; INCSP 1; GETSP;
   CSTI 0; SUB; GETBP; CSTI 3; ADD; LDI; CSTI 0; ADD; CSTI 0; STI; INCSP -1;
   INCSP 2; GETSP; CSTI 1; SUB; GETBP; CSTI 6; ADD; LDI; CSTI 0; ADD; CSTI 2;
   STI; INCSP -1; GETBP; CSTI 6; ADD; LDI; CSTI 1; ADD; CSTI 4; STI; INCSP -1;
   PRINTCURFRM [(0, "n"); (1, "p"); (3, "pn"); (6, "a")]; INCSP -7; RET -1]
```

## The mentioned MicroC examples:
### `exam1.c`
```c++
int g;
void main() {
    int n;
    int *p;
    int *pn[1];
    int a[2];
}
```

### `exam2.c`
```c++
int g;
void main()
{
    g=42;
    int n;
    n=1234;
    int *p;
    p = &n;
    int *pn[1];
    pn[0] = &g;
    int a[2];
    a[0] = 2;
    a[1] = 4;
    printCurFrame;
}
```