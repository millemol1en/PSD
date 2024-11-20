# Assignment_10


## Exercise 11.1
### i)
"_Write a continuation-passing (CPS) version `lenc : ’a list -> int
-> ’b) -> ’b` of the list length function len:_"

```fsharp
// Original:
let rec len xs =
    match xs with
    | [] -> 0
    | x::xr -> 1 + len xr;;

// CPS:
let lenc lst = 
    let rec aux l cF = 
        match l with
        | [] -> cF 0
        | _::xs -> aux xs (fun v -> cF(1 + v))

    aux lst id
```

### ii)
"_What happens if you call it as lenc xs (fun v -> 2*v) instead?_"

#### Answer:
We change our `id` function to write `let id = (fun v -> 2 * v)` wherein we will get back double the length in return:

```fsharp
// Altered id function:
let id = (fun v -> 2 * v)

// Example I/O:
lenc [2;3;4]
val it: int = 6

lenc [2;3;4;5;6;7]
val it: int = 12
```

### iii)
"_Write also a tail-recursive version leni : int list -> int -> int of the length function, whose second parameter is an accumulating parameter. The function should be called as leni xs 0. What is the relation between lenc and leni?_"

```fsharp
let leni lst =
    let rec aux l acc =
        match l with
        | [] -> acc
        | _::xs -> aux xs (acc + 1)
    
    aux lst 0
```

## Exercise 11.2
### i)
"_Write a continuation-passing version revc of the list reversal function rev:_"

```fsharp
let revc lst =
    let rec aux l cF =
        match l with
        | []    -> cF []
        | x::xs -> aux xs (fun v -> cF (v @ [x]))
    
    aux lst id
```


## ii)
"_What happens if you call it as revc xs (fun v -> v @ v) instead?_"

#### Answer:
We change our `id` function to write `let id = (fun v -> v @ v)` wherein we will get back double the number of returned elements:

```fsharp
// Altered id function:
let id = (fun v -> v @ v)

// Example I/O:
revc [2;3;4]
val it: int list = [4; 3; 2; 4; 3; 2]

revc [2;3;4;5;6]
val it: int list = [6; 5; 4; 3; 2; 6; 5; 4; 3; 2]
```

### iii)
"_Write a tail-recursive reversal function `revi : ’a list -> ’a list -> ’a list`, whose second parameter is an accumulating parameter, and which should be called as `revi xs [].`_"

```fsharp
let revi lst =
    let rec aux l acc =
        match l with
        | []    -> acc
        | x::xs -> aux xs (x :: acc)
    
    aux lst []
```

## Question 11.3
#### Question:
"_Write a continuation-passing version `prodc : int list ->
(int -> int) -> int` of the list product function `prod`:_"

```fsharp
// Original:
let rec prod xs =
    match xs with
    | [] -> 1
    | x::xr -> x * prod xr;;
    
    
// CPS:
let prodc lst =
    let rec aux l cF =
        match l with
        | []    -> cF 1
        | x::xs -> aux xs (fun v -> cF (v * x))
        
    aux lst id


// Example I/O:
prodc [1;2;3]
val it: int = 6

prodc [2;3;4]
val it: int = 24
```

## Question 11.4:
#### Part 1:
"_Optimize the CPS version of the prod function above. It could terminate as soon as it encounters a zero in the list (because any list containing a zero
will have product zero), assuming that its continuation simply multiplies the result
by some factor. Try calling it in the same two ways as the lenc function in Exercise 11.1. Note that even if the non-tail-recursive prod were improved to return 0
when encountering a 0, the returned 0 would still be multiplied by all the x values
previously encountered._"


The optimization simply checks to see whether or not the next value is `0` and if so immediately return `0`:
```fsharp
let prodc lst =
    let rec aux l cF =
        match l with
        | []    -> cF 1
        | x::_ when x = 0 -> 0
        | x::xs -> aux xs (fun v -> cF (v * x))
        
    aux lst id
```

#### Part 2:
"_Write a tail-recursive version prodi of the prod function that also terminates
as soon as it encounters a zero in the list._"

The same optimization but this time with the `acc` variant for continuation functions:
```fsharp
let prodi lst =
    let rec aux l acc =
        match l with
        | []    -> acc
        | x::_ when x = 0 -> 0
        | x::xs -> aux xs (acc * x)
    
    aux lst 1
```

## Question 11.8:
#### i)
"_Write an expression that produces and prints the values `3 5 7 9`. Write an expression that produces and prints the values `21 22 31 32 41 42`._"


```fsharp
// i_a
let exi = Every(
            Write(
                Prim("+", CstI 1,
                Prim("*", CstI 2,
                     FromTo(1, 4)))));

// i_b - 21 22 31 32 41 42
// This one is hella difficult for my dumbass...
```

#### ii)
```fsharp
let exii = Write(Prim("<", CstI 50, Prim("*", CstI 7, FromTo(1, 10))))
```

#### iii)
"_Extend the abstract syntax with unary (one-argument) primitive functions, like
this:_"


```fsharp
// Updated type 'expr':
type expr = 
  ...
  | Prim of string * expr * expr
  | Prim1 of string * expr
  ...
  | Fail;;

// Updated eval:
let rec eval (e : expr) (cont : cont) (econt : econt) = 
    match e with
    ...
    | Prim1 (ope, e) ->
          eval e (fun v -> fun econt1 ->
                match (ope, v) with
                | ("sqr", Int i1) ->
                    cont (Int(i1*i1)) econt1
                | ("even", Int i1) ->
                    // Like above, we add the possibility of backtracking with the use of continuation...
                    if i1 % 2 = 0 then 
                        cont (Int i1) econt1
                    else
                        econt1 ()
                | ("!", Int i1) ->
                    cont (Int(-i1)) econt1
                | _ -> Str "unknown prim1"
              )
              econt
              
// Some examples:
let exiii_1 = Every(Write(Prim1("!", FromTo(1, 4))))
> run exiii_1;;
-1 -2 -3 -4 val it: value = Int 0

let exiii_2 = Every(Write(Prim1("even", FromTo(1, 7))))
> run exiii_2;;
2 4 6 val it: value = Int 0

let exiii_3 = Write(Prim1("sqr", CstI 10))
> run exiii_3;;
100 val it: value = Int 100
```

#### iv)
"_Define a unary primitive multiples that succeeds infinitely many times, producing all multiples of its argument. For instance, multiples(3) should produce 3, 6, 9, . . . . Note that multiples(3 to 4) would produce multiples of 3 forever, and would never backtrack to the subexpression (3 to 4) to begin producing multiples of 4._"

```fsharp
// Updated eval:
let rec eval (e : expr) (cont : cont) (econt : econt) = 
    match e with
    ...
    | Prim1 (ope, e) ->
          eval e (fun v -> fun econt1 ->
                match (ope, v) with
                ...
                | ("multiplies", Int i1) ->
                    let rec infMult m =
                        let res = i1 * m
                        cont (Int res) (fun () -> infMult (res))    // From my understanding, we by-pass the normal backtracking by using the "fun ()" instead of econt1 as we do in all other examples above?
                    infMult i1
                | _ -> Str "unknown prim1"
              )
              econt
```