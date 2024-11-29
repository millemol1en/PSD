# BSWU BPRD 2018 Jan
## Opgave 1 (20%): Regulære udtryk og automater
![Alt Text](./SolutionPictures/NFA-to-DFA%20Question.png)

### Question 1:
#### Spørgsmål:
"_Angiv alle årsager til at automaten er ikke–deterministisk (så er NFA)._"

#### Svar:
- Node `3` has 2 out-going edges with `a` as the state change.
- There is an epsilon between nodes 2 and 3.

### Question 2:
#### Spørgsmål:
"_Giv tre eksempler på strenge der genkendes af automaten._"

#### Svar:
- 1: `aa`
- 2: `abbbba`
- 3: `aaba`

### Question 3:
#### Spørgsmål:
"_Giv en uformel beskrivelse af sproget (mængden af alle strenge) der beskrives af automaten_"

#### Svar:
All the strings which can be accepted by this NFA must start with an `a` and end with an `a`

### Question 4:
#### Spørgsmål:
"_Konstruer og tegn en deterministisk endelig automat (“deterministic finite automaton”, DFA) der svarer til automaten ovenfor. Husk at angive starttilstand og accepttilstand(e). Du skal enten bruge en systematisk konstruktion svarende til den i forelæsningen eller som i Introduction to Compiler Design (ICD), eller Basics of Compiler Design (BCD), eller forklare hvorfor den resulterende automat er korrekt._"


#### Svar:
##### Step 1: Remove the Epsilon:

|   | a     | b | ε     |
|---|-------|---|-------|
| 1 | 2     | Ø | 1     |
| 2 | Ø     | 2 | {2,3} |
| 3 | {4,5} | Ø | 3     |
| 4 | Ø     | 3 | 4     |
| 5 | Ø     | Ø | 5     |

As there is an epsilon going from node `2` to `3`, we have to merge the contents of `3` into `2`. Remember, node `3` will still keep its contents:

|   | a     | b |
|---|-------|---|
| 1 | 2     | Ø |
| 2 | {4,5} | 2 |
| 3 | {4,5} | Ø |
| 4 | Ø     | 3 |
| 5 | Ø     | Ø |

The diagram for this would be. **Note**, we are not yet changing the nodes themselves, so `{4, 5}` still represents the separate nodes `4` and `5`.
![Alt Text](./SolutionPictures/NFA-to-DFA%20No%20Epsilon.png)

##### Step 2: Convert the new NFA to a DFA:
Using the newly created table from above, we create yet another table (_representing the states of our DFA_) and from it create a drawing:

|                          | a                        | b                    |
|--------------------------|--------------------------|----------------------|
| 1 := (S<sub>0</sub>)     | 2 := (S<sub>1</sub>)     | Ø                    |
| 2 := (S<sub>1</sub>)     | {4,5} := (S<sub>3</sub>) | 2 := (S<sub>1</sub>) |
| 3 := (S<sub>2</sub>)     | {4,5} := (S<sub>3</sub>) | Ø                    |
| 4 := N/A                 | Ø                        | 3 := (S<sub>2</sub>) |
| 5 := N/A                 | Ø                        | Ø                    |
| {4,5} := (S<sub>3</sub>) | Ø                        | 3 := (S<sub>2</sub>) |

![Alt Text](./SolutionPictures/NFA-to-DFA%20final.png)

### Question 5:
#### Spørgsmål:
"_Angiv et regulært udtryk der beskriver mængden af strenge over alfabetet {a, b}, som beskrives af automaten ovenfor. Check og forklar at det regulære udtryk også beskriver mængden af strenge for din DFA._"

#### Svar:
`a(b)*(ab)*a`


## Opgave 2 (25%): Icon
### Question 1:
#### Spørgsmål:
"_Omskriv eksemplet iconEx1, så værdierne 3 4 3 4 udskrives:_"

#### Svar:
```fsharp
// Option 1:
let examIcon1 = Every(Write(Or(FromTo(3, 4), FromTo(3, 4))))

// Option 2:
let examIcon2 = Every(Write(And(FromTo(1, 2), FromTo(3,4))))
```

### Question 2:
#### Spørgsmål:
"_Skriv et eksempel som udskrives de følgende værdier `"I" "c" "o" "n"`_"

#### Svar:
```fsharp
// Option 1: Concat string together with OR
let examIcon3 = Every(Write(Or(CstS "I", Or(CstS "C", Or(CstS "O", CstS "N")))))
```


### Question 3:
#### Spørgsmål:
"_Skriv et eksempel som udskrives de følgende værdier `"I" "c" "o" "n"`_"

#### Svar:
The code for this is based off of the match-case for `FromTo(i1, i2)` from `eval` function.
```fsharp
// Solution:
| Bang s ->
        let sLst = [for c in s -> string c]
        
        let rec aux lst =
            match lst with
            | []    -> econt()
            | x::xs -> cont (Str x) (fun () -> aux xs)
        
        aux sLst

// Example:
let exampleBang = Every(Write(Bang("Icon")));;
```


### Question 4:
#### Spørgsmål:
"_Udvid implementationen af Icon med en ny generator `BangN(str,n)`, som fungerer som Bang(str) ovenfor med den udvidelse at strengen str gentages n gange. Generatoren `Bang(str)` giver identisk uddata som `BangN(str, 1)`._"

#### Svar:
The code for this is based off of the match-case for `FromTo(i1, i2)` from `eval` function - in particular the `if-else` block.
```fsharp
// Solution: 
| BangN (s, n) ->
        let rec loop iter = 
          if iter > 0 then 
              eval (Bang s) cont (fun () -> loop (iter - 1))
          else 
              econt ()
        loop n
        
// Example:
let exampleBangN = Every(Write(BangN("Icon", 3)))
```

## Opgave 3 (25%): Enums in Micro-ML
### Opgave Beskrivelse:
"_Opgaven gå ud på at udvide funktionssproget med muligheden for at avende enums med følgende type_"

```c++
enum Weekend = Sat | Sun in
    Weekend.Sat
end
```

### Question 1:
#### Spørgsmål:
"_Tegn et evaluerings træ, med reglerne i figur 4.3 (PLC) samt e10 og e11 for udtrykket nedenfor._"

![Alt Text](./SolutionPictures/Derivation%20Tree%20Rules%202.png)

#### Svar:
![Alt Text](./SolutionPictures/Derivation%20Tree%20Solution.png)

### Question 2:
#### Spørgsmål:
"_Vi udvider den abstrakte syntaks (fil `Absyn.fs` fra lektion 3 og 4) med support for enumerations således_"

```fsharp
type expr =
    | CstI of int
    ...
    | Enum of string * string list * expr (* Exam *)
    | EnumVal of string * string (* Exam *)
    ...
```

"_`Enum(n,vs,e)` repræsenterer en enumeration navngivet `n` med de mulige værdier `vs` som en liste af strenge
samt udtrykket `e` hvori den kan bruges. `EnumVal(n,v)` repræsenterer udvælgelsen af den angivne værdi `v`
for enumeration `n`. Med eksemplet øverst i opgaven vil den abstrakte syntaks være:_"

```fsharp
Enum ("Weekend",["Sat"; "Sun"],EnumVal ("Weekend","Sat"))
enum Weekend = Sat | Sun in Weekend.Sat end
```

"_Skriv og check for det følgende eksempel at det virker:_"

```fsharp
enum Weekend = Sat | Sun in let r = 1 + Weekend.Sun in r + 1 end end
```

#### Svar:
```fsharp
let enumEx = Enum ("Weekend", ["Sat"; "Sun"],
                     Let ("r",
                          Prim("+", CstI 1, EnumVal ("Weekend", "Sun")),
                          Prim("+", Var "r", CstI 1)))
```


### Question 3:
#### Spørgsmål:
"_Udvid lexer og parser så den godkender vores nye Enum type_"

"_**Hint!**_ _For at parse E<sub>0</sub> | · · · | E<sub>n</sub> kan du med fordel se hvorledes parametre til en funktion parses i `microC`. Se reglerne `Paramdecs` og `Paramdecs1` i filen `CPar.fsy` (lektion 6). Verificer at din parser giver samme abstrakte syntaks som den du lavede i opgave 2 ovenfor._"

#### Svar:
#### File `FunLex.fsl`
```text
let keyword s =
    match s with
    ...
    | "enum"  -> ENUM
    | _       -> NAME s
}

rule Token = parse
  | [' ' '\t' '\r'] { Token lexbuf }
  ...
  | '|'             { BAR }
  | '.'             { DOT }
  ...
  | _               { failwith "Lexer error: illegal symbol" }
```

#### File `FunPar.fsy`
```text
%token ENUM DOT BAR

Expr:
    AtExpr                              { $1                     }
  | AppExpr                             { $1                     }
  ...
  | NAME DOT   NAME                     { EnumVal($1, $3)        }
;

AtExpr:
    Const                               { $1                     }
  | NAME                                { Var $1                 }
  ...
  | ENUM NAME EQ EnumVal IN Expr END    { Enum($2, $4, $6)       }
;

(* The following is entirely copied from MicroC parser CPar.fsy *)
EnumVal:
    /* empty */                         { []                     }
  | EnumVal1                            { $1                     }
    
EnumVal1:
    NAME                                { [$1]                   }
  | NAME BAR EnumVal1                   { $1 :: $3               } 
```

```fsharp
> fromString @"enum Weekend = Sat | Sun in let r = 1 + Weekend.Sun in r + 1 end end";;
val it: Absyn.expr =
  Enum
    ("Weekend", ["Sat"; "Sun"],
     Let
       ("r", Prim ("+", CstI 1, EnumVal ("Weekend", "Sun")),
        Prim ("+", Var "r", CstI 1)))
```


"_Til sidst, vis også den abstrakte syntaks for nedenstående eksempler og forklar hvorfor de er som forventet:_"

- `enum OneTwo = One | Two in OneTwo.One end`
- `enum One = One in One.One end`
- `enum None = in 42 end`

```fsharp
> fromString @"enum OneTwo = One | Two in OneTwo.One end";;
val it: Absyn.expr = Enum ("OneTwo", ["One"; "Two"], EnumVal ("OneTwo", "One"))

> fromString @"enum One = One in One.One end";;
val it: Absyn.expr = Enum ("One", ["One"], EnumVal ("One", "One"))

> fromString @"enum None = in 42 end";;
val it: Absyn.expr = Enum ("None", [], CstI 42)
```

### Question 4:
#### Spørgsmål:
"_Udvid funktionen eval i `HigherFun.fs`, med evaluering af enumerations._"


#### Svar:

```fsharp
let rec eval (e : expr) (env : value env) : value =
    match e with
    ...
    | Enum (eName, eVals, eBody) ->
      let eEnv = (eName, EVal (eName, eVals)) :: env 
      eval eBody eEnv
    | EnumVal (n, v) ->
      let res = lookup env n
      match res with
      | EVal (eN, eL) when eN = n ->
        Int (List.findIndex (fun target -> target = v) eL)
      | _ -> failwith "Could not locate an Enum or was the incorrect type"
```

"_Køre det følgende eksempel og check at det giver resultatet `0`_"

```fsharp
> fromString @"enum Weekend = Sat | Sun in Weekend.Sat end";;
val it: Absyn.expr = Enum ("Weekend", ["Sat"; "Sun"], EnumVal ("Weekend", "Sat"))

> run it;; 
val it: HigherFun.value = Int 0
```

## Opgave 4 (20%): MicroC Genereret Bytekode:
### Question 1:
#### Spørgsmål:
"_Betragt følgende micro–C program `microcEx2.c`:_"

```c++
void main() {
    int i;
    i = 3;
    print f(i);
}
int f(int n) {
    int i;
    i = 42;
    /* Describe stack content at this point. */
    return n+i;
}
```

"_Tegn og beskriv indholdet af stakken når programafviklingen når til det sted, hvor kommentaren er indsat
i funktionen `f`. Du skal opdele stakken i stack frames og angive base pointer og stack pointer._"

#### Svar:

```text
_____________________
|                   |
|                   |           |
|                   |           |
|                   |           |
|  Addr of PRINTI   |
_____________________
|      3 (i)        |   <-- BP  |
|      -999         |           |> Main stack frame
|        4          |           |
_____________________

```



### Question 2:
"_Micro–C oversætteren Contcomp.fs fra kapitel 12 i PLC genererer følgende bytekode for programmet ovenfor_"

```fsharp
LDARGS;         // Load commandline arguments - there are none
CALL (0,"L1")   // Call Main with 0 arguments.
STOP            // Return from Main and end program.
Label "L1"      // Label for Main
INCSP 1         // Make room for 'i' on the stack 
GETBP           // Get Base Pointer, which is the address of 'i'
CSTI 3          // Place a constant 3 on top of the stack
STI             // Set the value of 'i' to this constant 3
INCSP -1        // Remove 3 from the stack (it is kept in storage at the address of 'i')
GETBP           // Get Base Pointer again, this is still address of 'i'
LDI             // We load the value at the address - giving us 3
CALL (1,"L2")   // We call the function at label 2 - in this case it is "f"
PRINTI          // We print the subsequent value returned by the function "f"
RET 1           // We return from the function "main" - the "1" indicates the number of local variables to remove.
Label "L2"      // The label for function "f".
INCSP 1         // Make space to place variable 'i' onto the stack.
GETBP           // Get the Base Pointer (it still points to the 'i' from "main").
CSTI 1          // Place a constant of 1 on the stack.
ADD             // Add them together to then have the Base Pointer be BP + 1 - this will add the new locally defined 'i' (in function "f") to be on the top of the stack.
CSTI 42         // Load a constant value of 42 onto the top of the stack.
STI             // Store the value of 42 onto the address (42 is on top of the stack and right under is addr of BP + 1).
INCSP -1        // We decrement the stack pointer, removing the constant 42.
GETBP           // We get the BP, in this case it points to the original variable 'i' from "main" which now is represented by the argument 'n'.
LDI             // We load the value, placing it ontop of the stack
GETBP           // We get the BP
CSTI 1          // Place a constant of 1 ontop of the stack
ADD             // Add them together to get BP + 1
LDI             // Load that value onto the top of the stack
ADD             // And subsequently add the 2 values 'i' and 'n' together
RET 2           // We return from function "f", removing variables 'i' and 'n'.
```

### Question 3:
#### Spørgsmål:
"_Opgaven er at begrænse hvor meget af den abstrakte syntaks der bliver printet når vi beregner udtryk der inholder `+`, `-`, `*` og `/`. Vi kommer til at arbejde med det følgende microC program `microCExam2.c`_"

```c++
void main() {
   print f(100000);
}

int f(int n) {
   int r;
   r = 0;
   while (n > 0) {
      r = r + (42*2-34*2+32+3);
      n = n-1;
   }
   return r;
}
```

```fsharp
// Hvad vi har:
Assign(AccVar "r",
    Prim2("+",Access (AccVar "r"),
        Prim2("+",Prim2("+",Prim2("-",Prim2("*",CstI 42,CstI 2),
        Prim2("*",CstI 34,CstI 2)),
        CstI 32), 
    CstI 3)))

// Hvad vi gerne vil have:
Assign(AccVar "r", Prim2("+",Access (AccVar "r"),CstI 51))
```

#### Svar:
**Remember!** Use what is already there. This seem incredibly difficult but the majority of it was given and much of the code is already there amongst the other functions / operations:

```fsharp
let rec reducePrim2 e =
    let e' =
        match e with
          // [!] Notice, we don't have a match-case for `Prim2` instead we perform the necessary logic and always go to the match-case "| e -> e"
          Prim2 (ope, e1, e2) ->
            // [0] Dig as deep as we can go - like DFS:
            let e1' = reducePrim2 e1
            let e2' = reducePrim2 e2
            
            // [1] Match the operand and variables - reduce their values accordingly:
            match (ope, e1', e2') with
            | ("+", CstI e1', CstI e2') -> CstI (e1' + e2')
            | ("*", CstI e1', CstI e2') -> CstI (e1' * e2')
            | ("-", CstI e1', CstI e2') -> CstI (e1' - e2')
            | ("/", CstI e1', CstI e2') -> CstI (e1' / e2')
            | _ -> Prim2 (ope, e1', e2')    // do nothing
            
        | e -> e
    if e <> e' then printfn "ReducePrim2: Expression %A reduced to %A" e e'
    e'
```

Below is the example code `microcEx1.c` being compiled:
```text
runContComp
> open ParseAndContcomp;;
> contCompileToFile (fromFile "./Exercises/microCExam2.c") "./Exercises/microCExam2.out";;
ReducePrim2: Expression Prim2 ("*", CstI 42, CstI 2) reduced to CstI 84
ReducePrim2: Expression Prim2 ("-", Prim2 ("*", CstI 42, CstI 2), CstI 34) reduced to CstI 50
ReducePrim2: Expression Prim2 ("*", Prim2 ("-", Prim2 ("*", CstI 42, CstI 2), CstI 34), CstI 2) reduced to CstI 100
ReducePrim2: Expression Prim2 ("+", Prim2 ("*", Prim2 ("-", Prim2 ("*", CstI 42, CstI 2), CstI 34), CstI 2), CstI 32) reduced to CstI 132
ReducePrim2: Expression Prim2 ("+", Prim2 ("+", Prim2 ("*", Prim2 ("-", Prim2 ("*", CstI 42, CstI 2), CstI 34), CstI 2), CstI 32), CstI 3) reduced to CstI 135

val it: Machine.instr list =
  [LDARGS; CALL (0, "L1"); STOP; Label "L1"; CSTI 100000; CALL (1, "L2");
   PRINTI; RET 0; Label "L2"; INCSP 1; GETBP; CSTI 1; ADD; CSTI 0; STI;
   INCSP -1; GOTO "L4"; Label "L3"; GETBP; CSTI 1; ADD; GETBP; CSTI 1; ADD;
   LDI; CSTI 135; ADD; STI; INCSP -1; GETBP; GETBP; LDI; CSTI 1; SUB; STI;
   INCSP -1; Label "L4"; GETBP; LDI; CSTI 0; SWAP; LT; IFNZRO "L3"; GETBP;
   CSTI 1; ADD; LDI; RET 2]
```

Running the file `microcEx1.c` with the Java machine gives the following:
```text
java Machine ./Exercises/microCExam2.out
13500000 
Ran 0.016 seconds
```


## Opgave 5
### Question 1:
#### Spørgsmål:
"_Du skal nu udvide den abstrakte maskine til at opsamle og udskrive tilsvarende statistik efter kørsel af et program. Du bestemmer selv om du vil udvide maskinen implementeret i C (`machine.c`) eller Java (`Machine.java`)._"


#### Svar:
A global variable containing the number of occurrences for each instruction:
```c++
int instr_tally[26];
```

The function `execcode` now also tallies up all the instances of each instruction and stores it in the global variable `instr_tally`:
```c++
int execcode(int p[], int s[], int iargs[], int iargc, int /* boolean */ trace) {
  int bp = -999;	// Base pointer, for local variable access 
  int sp = -1;	        // Stack top pointer
  int pc = 0;		// Program counter: next instruction
  for (;;) {
    if (trace) 
      printStackAndPc(s, bp, sp, p, pc);
    switch (p[pc++]) {
    case CSTI:
      instr_tally[CSTI]++;
      s[sp+1] = p[pc++]; sp++; break;
    case ADD:
      instr_tally[ADD]++;
      s[sp-1] = s[sp-1] + s[sp]; sp--; break;
    case SUB:
      instr_tally[SUB]++;
      s[sp-1] = s[sp-1] - s[sp]; sp--; break;
```

The function `printInfo` has been created to run through the array `instr_tally` and print the number of occurrences for each instruction:
```c++
// Snippet of function:
void printInfo() {
  printf("Byte Code Statistics:\n");
  
  for (int i = 0; i < 26; i++)
  {
    switch (i) {
    case CSTI:   printf("CSTI   :: %d\n", instr_tally[i]); break;
    case ADD:    printf("ADD    :: %d\n", instr_tally[i]); break;
    case SUB:    printf("SUB    :: %d\n", instr_tally[i]); break;
    case MUL:    printf("MUL    :: %d\n", instr_tally[i]); break;
    case DIV:    printf("DIV    :: %d\n", instr_tally[i]); break;
      
// Placement:
int execute(int argc, char** argv, int /* boolean */ trace) {
  int *p = readfile(argv[trace ? 2 : 1]);           // program bytecodes: int[]
  ...
  printf("Used %7.3f cpu seconds\n", runtime);
  printInfo();                                      // Placed right after CPU run time
  return res;
}
```

### Question 2:
#### Spørgsmål:
"_Vis den fulde statistik af at køre microcEx1.c med din løsning._"



#### Svar:
Below is an example of how the print looks when running the byte code for the program `microcEx1.c`:
```text
Byte Code Statistics:
CSTI   :: 500005
ADD    :: 300002
SUB    :: 100000
MUL    :: 0
DIV    :: 0
MOD    :: 0
EQ     :: 0
LT     :: 100001
NOT    :: 0
DUP    :: 0
SWAP   :: 100001
LDI    :: 300002
STI    :: 200001
GETBP  :: 500003
GETSP  :: 0
INCSP  :: 200002
GOTO   :: 1
IFZERO :: 0
IFNZRO :: 100001
CALL   :: 2
TCALL  :: 0
RET    :: 0
PRINTI :: 1
PRINTC :: 0
LDARGS :: 1
STOP   :: 1
```