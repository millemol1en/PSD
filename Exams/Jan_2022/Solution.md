## Opgave 1 (25%) Micro–ML: Sets
### Opgave Beskrivelse:
"_Opgaven er at udvide funktionssproget med mængder (eng. sets), således at vi kan evaluere udtryk der manipulerer med mængder, se eksempel `ex01` nedenfor:_"

```fsharp
let s1 = {2, 3} in
    let s2 = {1, 4} in
        s1 ++ s2 = {2,4,3,1}
    end
end
```

"_Der bliver tilføjet det følgende:_"
    
- En ikke tom mængde: `{e1, . . . , en}, n ≥ 1`.
- En operator `e1 ++ e2` som laver union på to mænger repræsenteret ved udtrykkene `e1` og `e2`. Operator `++`
  har samme _**præcedens og associativitet**_ som operatoren `+`.

### Question 1:
#### Spørgsmål:
"_Du skal udvide lexer `FunLex.fsl` og parser `FunPar.fsy` med support for mængder og `++` operatoren defineret ovenfor. Den abstrakte syntaks i `Absyn.fs` er udvidet med Set der repræsenterer et mængdeudtryk._"

#### Svar:
Changes made to `FunLex.fsl`
```fsharp
rule Token = parse
  ...
  | '{'             { LCPAR }
  | '}'             { RCPAR }
  | ','             { COMMA }
  ...
```

Changes made to `FunPar.fsy`
```fsharp
Expr:
    AtExpr                              { $1                     }
  ...
  | Expr PLUSPLUS Expr                  { Prim("++", $1, $3)     }
  | LCPAR Set RCPAR                     { Set($2)                }
;

Set:
  | Expr                                { [$1]                   }
  | Expr COMMA Set                      { $1 :: $3               }
;
```

Note! Unless told otherwise, in `microC` it is best to assume that everything is an expression. What I mean by this is, for the `++` operator, we only want it to work for `Sets`. If we were to put that constraint on the parser it would require a lot more code - instead we will do that during evaluation. 

Running the given example `ex01` results in the desired output as seen below:
```fsharp
> fromString @"let s1 = {2, 3} in let s2 = {1, 4} in s1 ++ s2 = {2,4,3,1} end end";;
val it: expr =
  Let
    ("s1", Set [CstI 2; CstI 3],
     Let
       ("s2", Set [CstI 1; CstI 4],
        Prim
          ("=", Prim ("++", Var "s1", Var "s2"),
           Set [CstI 2; CstI 4; CstI 3; CstI 1])))
```
"_Vis ydermere at du ikke kan parse nedenstående eksempel med en tom mængde:_"

```fsharp
> fromString @"let s = {} in s end";;                                            
System.Exception: parse error near line 1, column 10
```

### Question 2:
#### Spørgsmål:
"_Angiv et evalueringstræ for udtrykket:_"

```fsharp
let s = {1, 2} in s ++ {3} end
```

"_Brug de følgende regler til din evalueringstræ_"

Fig. 1: Rule for creating a set
![alt text](./SolutionPictures/Derivation%20Tree%20Set%20Rule.png)

Fig. 2: Rule for comparing 2 sets
![alt text](./SolutionPictures/Derivation%20Tree%20Equals%20Rule.png)

Fig. 3: Rule for performing addition (_union_) on 2 sets
![alt text](./SolutionPictures/Derivation%20Tree%20PlusPlus%20Rule.png)


#### Svar:



### Question 3:
#### Spørgsmål:
"_Udvid typen value og funktionen `eval` i `HigherFun.fs`, således at udtryk med mængder kan evalueres
som defineret af reglerne ovenfor. Vi repræsenterer mængder med den indbyggede Set–type i F#, som både
understøtter foreningsmængde, `Set.union`, og lighed `=`._"

```fsharp
type value =
  | Int of int
  | Closure of string * string * expr * value env (* (f, x, fBody, fDeclEnv) *)
  | SetV of Set<value> (* Exam *)
```

"_Det er vigtigt, at du beskriver din implementation. Vis at din implementation virker med eksemplet `ex01` ovenfor._"

#### Svar:
As described in the assignment, the `++` operator is equivalent to the union of 2 sets. As such, we simply use the in-house F# function `Set.union` to mash together the 2 sets.

Changes made to `HigherFun.fs`
```fsharp
let rec eval (e : expr) (env : value env) : value =
    match e with
    ...
    | Prim(ope, e1, e2) -> 
      let v1 = eval e1 env
      let v2 = eval e2 env
      match (ope, v1, v2) with
      ...
      | ("=", SetV set1, SetV set2) -> Int (if (Set.isSubset set1 set2) && (Set.isSubset set2 set1) then 1 else 0)
      | ("++", SetV set1, SetV set2) -> SetV (Set.union set1 set2)
      ...
      |  _ -> failwith "unknown primitive or wrong type"
    ...
    | Set (exprLst) ->
      
      let rec evaluator (eLst : expr list) (acc : Set<value>) =
        match eLst with
        | []    -> acc
        | x::xs -> evaluator xs (Set.add (eval x env) acc)
      
      SetV (evaluator exprLst Set.empty)
```

Running the test cases:
```fsharp
// Running ex01
> fromString @"let s = {1, 2} in s ++ {3} end";;
val it: expr =
  Let ("s", Set [CstI 1; CstI 2], Prim ("++", Var "s", Set [CstI 3]))
> run it;;
val it: HigherFun.value = SetV (set [Int 1; Int 2; Int 3])

// Not equal sets:
> fromString @"let s = {1, 2} in s = {1, 3} end";;
val it: expr =
  Let ("s", Set [CstI 1; CstI 2], Prim ("=", Var "s", Set [CstI 1; CstI 3]))
> run it;;
val it: HigherFun.value = Int 0

// Equal sets:
> fromString @"let s = {1, 2} in s = {1, 2} end";;
val it: expr =
  Let ("s", Set [CstI 1; CstI 2], Prim ("=", Var "s", Set [CstI 1; CstI 2]))
> run it;;
val it: HigherFun.value = Int 1

// Another addition:
> fromString @"let s = {1, 2} in s ++ {1, 2, 3, 4, 5} end";;
val it: expr = Let ("s", Set [CstI 1; CstI 2], Prim ("++", Var "s", Set [CstI 1; CstI 2; CstI 3; CstI 4; CstI 5]))
> run it;;
val it: HigherFun.value = SetV (set [Int 1; Int 2; Int 3; Int 4; Int 5])
```


## Opgave 2 (30%) Micro–C: Print Stack
### Opgave Beskrivelse:
"_I denne opgave tilføjer vi et nyt statement `printStack e`, som udskriver stakken på skærmen. Til det, vil vi bruge det følgende eksempel `fac.c`_"

```c++
int nFac;
int resFac;
void main(int n) {
  int i;
  i = 0;
  nFac=0;
  while (i < n) {
    resFac = fac(i);
    i = i + 1;
  }
  printStack 42;
}
int fac(int n) {
  nFac = nFac + 1;
  printStack nFac;
  if (n == 0)
    return 1;
  else
    return n * fac(n-1);
}
```

"_Skabelonen for vores `printStack` funktion ser sådan her ud:_"
```text
-Print Stack <e>----------------
Stack Frame
Lokale variable og Temporære værdier
Base peger
Retur adresse
Stack Frame
...
Global
Globale variable
```

The format is as follows:
Formatteringen defineres således 
 - Lokale variable og temporære værdier: `s[addr]: Local/Temp = s[addr]`
 - Basepeger: `s[addr]: bp = s[addr]`
 - Returadresse: `s[addr]: ret = s[addr]`
 - Global variabel: `s[addr]: s[addr]`

"_Med dette skabelon, vil `fac.c` printe det følgende information:_"

```text
MicroC % java Machine fac.out 1
-Print Stack 1----------------
Stack Frame
s[9]: Local/Temp = 0
s[8]: bp = 4
s[7]: ret = 39
Stack Frame
s[6]: Local/Temp = 1
s[5]: Local/Temp = 0
s[4]: Local/Temp = 1
s[3]: bp = -999
s[2]: ret = 8
Global
s[1]: 0
s[0]: 1
-Print Stack 42----------------
Stack Frame
s[5]: Local/Temp = 1
s[4]: Local/Temp = 1
s[3]: bp = -999
s[2]: ret = 8
Global
s[1]: 1
s[0]: 1
Ran 0.028 seconds
MicroC %
```

### Question 1:
#### Spørgsmål:


#### Svar:



**Note!** In the abstract syntax provided, the sequence `Stmt (PrintStack (CstI 42))]);` indicates that `PrintStack` should be a statement and evaluate an expression `e`.


```text
> fromFile @"./Examples/exam.c";;
System.Exception: parse error in file ./Examples/exam.c near line 11, column 18
```

No worries. I go to the file `exam.c` and see that on line 11 column 18 there is a semicolon. I go to the parser `CPar.fsy` and see that I forgot to add a semicolon.

```fsharp
StmtM:  /* No unbalanced if-else */
    Expr SEMI                           { Expr($1)       }     
  ...
  | PRINTSTACK Expr SEMI                { PrintStack($2) }
;
```

**Notice!** There are 2 crucial things to note here:
 - Expression `Expr` are all already equipped with a semicolon `SEMI`.
 - Our `PRINTSTACK` instruction is a statement `StmtM` and therefore needed its own semicolon.










## Opgave 3 (25%) Micro–C: Intervalcheck
### Opgave Beskrivelse:
"_Opgaven er at udvide micro–C med intervalcheck, som er udtryk af formen `e` within `[e1,e2]` der returnerer sand (`1`) eller falsk (`0`) afhængig af om `v1 ≤ v ≤ v2` er opfyldt, hvor `v`, `v1` og `v2` er resultaterne af at evaluere `e`, `e1` og `e2`. Der er følgende semantiske krav til implementationen:_"

_Der er følgende semantiske krav til implementationen:_ 
 - _Udtrykkene `e`, `e1` og `e2` skal altid evalueres præcis en gang._ 
 - _Nøgleordet `within` har samme præcedens som andre sammenligningsoperatorer, f.eks. `<` og `>`_.

"_I den abstrakte syntaks repræsenteres et intervalcheck med `WithIn (e, e1, e2)`, hvor `e`, `e1` og `e2` er vilkårlige
udtryk. Vi vil bruge det følgende eksempel `within.c` som mål for denne opgave:_"

```c++
void main() {
  print (0 within [print 1,print 2]); // Expected output: 1 2 0
  print (3 within [print 1,print 2]); // Expected output: 1 2 0
  print (print 42 within [print 40,print 44]); // Expected output: 40 44 1 1
  print ((print 42) within [print 40,print 44]); // Expected output: 42 40 44 1
}
```

### Question 1:
#### Spørgsmål:
"_Du skal udvide lexer `CLex.fsl`, parser `CPar.fsy` og `Absyn.fs` med support for udtrykket `e` within
`[e1,e2]`. Du ser den abstrakte syntaks for ovenstående eksempel `within.c` nedenfor:_"

```fsharp
> fromFile "within.c";;
val it : Absyn.program =
Prog [Fundec (None,"main",[],
    Block [Stmt (Expr (Prim1 ("printi", WithIn (CstI 0,
                Prim1("printi",CstI 1),
                Prim1 ("printi",CstI 2)))));
          Stmt (Expr (Prim1 ("printi", WithIn (CstI 3,
                Prim1 ("printi",CstI 1),
                Prim1 ("printi",CstI 2)))));
          Stmt (Expr (Prim1 ("printi", Prim1 ("printi", WithIn (CstI 42,
                Prim1("printi",CstI 40),
                Prim1("printi",CstI 44))))));
          Stmt (Expr (Prim1 ("printi", WithIn (Prim1 ("printi",CstI 42),
                Prim1 ("printi",CstI 40),
                Prim1 ("printi",CstI 44)))))])]
```

#### Svar:
Changes made to `CPar.fsy`
```text
%token PRINTSTACK WITHIN

...

%left GT LT GE LE WITHIN

...

ExprNotAccess:
    AtExprNotAccess                     { $1                  }
  ...
  | Expr WITHIN LBRACK Expr COMMA Expr RBRACK { WithIn($1, $4, $6) }
;
```


Changes made to `CLex.fsl`
```text
let keyword s =
    match s with
    ...
    | "within"  -> WITHIN
    | _         -> NAME s
```


**Note!** For these questions always look at the provided abstract syntax as it helps determine what type the thing we are adding needs to be. As it is located within a `Prim1`, we can assume it must be an expression as that is the only thing `Prim1` accepts. 

### Question 2:
#### Spørgsmål:
"_For at implementere oversættelsen af within–udtrykket i `Comp.fs` skal du lave et oversætterskema svarende til dem du finder i figur 8.6 i PLC. Du må kun anvende de bytekodeinstruktioner, som allerede findes i bytekodefortolkeren, se figur 8.1 i PLC._"

**Husk!** "_Forklar hvorfor dit oversætterskema opfylder de semantiske krav givet ovenfor._"

#### Svar:




### Question 3:
#### Spørgsmål:
"_Du kan nu anvende dit oversætterskema ovenfor til at implementere oversættelsen af within–udtrykket i Comp.fs: Det er vigtigt, at du beskriver din implementation. Vis at din implementation fungerer ved at vise uddata ved kørsel af programmet within.c. I fald dit uddata ikke svarer helt til det forventede kan du beskrive hvad du tænker der er anderledes i din løsning._"


#### Svar:

##
### Question 1:
#### Spørgsmål:


#### Svar:



