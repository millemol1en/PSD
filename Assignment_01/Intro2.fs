module PSD.Assignment_01.Intro2

let env = [ ("a", 3); ("c", 78); ("baf", 666); ("b", 111) ]

let emptyenv = [] (* the empty environment *)

let rec lookup env x =
    match env with
    | [] -> failwith (x + " not found")
    | (y, v) :: r -> if x = y then v else lookup r x

let cvalue = lookup env "c"

(* Object language expressions with variables *)

type expr =
    | CstI of int
    | Var of string
    | Prim of string * expr * expr
    | If of expr * expr * expr

let e1 = CstI 17
let e2 = Prim("min", CstI 3, Var "a")
let e3 = Prim("min", Prim("*", Var "b", CstI 9), Var "a")
let e4 = Prim("max", CstI 2, CstI 5)
let e5 = Prim("min", CstI 2, CstI 5)
let e6 = Prim("==", CstI 2, CstI 2)
let e7 = Prim("==", CstI 2, CstI 5)
let e8 = If(Var "a", CstI 11, CstI 22)

///////////////////////////
///                     ///
///     EXERCISE 1.1    ///
///                     ///
///////////////////////////
let rec eval e (env: (string * int) list) : int =
    match e with
    | CstI i -> i
    | Var x -> lookup env x
    | Prim(ope, e1, e2) ->
        let i1 = eval e1 env
        let i2 = eval e2 env

        match ope with
        | "+" -> i1 + i2
        | "-" -> i1 - i2
        | "*" -> i1 * i2
        | "max" -> if i1 > i2 then i1 else i2
        | "min" -> if i1 < i2 then i1 else i2
        | "==" -> if i1 = i2 then 1 else 0
        | _ -> failwith "operator doesn't exist in expr"
    | If(e1, e2, e3) ->
        let eRes1 = eval e1 env
        
        if eRes1 >= 0 then
            eval e2 env
        else
            eval e3 env
    | Prim _ -> failwith "unknown primitive"


let e1v = eval e1 env
let e2v1 = eval e2 env
let e2v2 = eval e2 [ ("a", 314) ]
let e3v = eval e3 env

///////////////////////////
///                     ///
///     EXERCISE 1.2    ///
///                     ///
///////////////////////////
// Note: We changed the names of Var and CstI as to not get issues between the two types of expressions.
// (i)
type aexpr =
    | Const of int
    | AVar of string
    | Add of aexpr * aexpr
    | Mul of aexpr * aexpr
    | Sub of aexpr * aexpr
    
// (ii)
let e9 = Sub(AVar "v", Add(AVar "w", AVar "z"))                     // v - (w + z)
let e10 = Mul(Const 2, Sub(AVar "v", Add(AVar "w", AVar "z")))      // 2 ∗ (v − (w + z))
let e11 = Add(AVar "x", Add(AVar "y", Add(AVar "z", AVar "v")))     // x + y + z + v
let e12 = Add(AVar "x", Const 0)
let e13 = Mul(Const 0, AVar "x")
let e14 = Sub(Const 10, Const 10)
let e15 = Sub(Const 10, Const 0)
let e16 = Mul(Const 2, Const 5)
let e17 = Mul(Const 2, Sub(Const 10, Const 0))

// (iii)
let rec fmt (e: aexpr) : string =
    match e with
    | Const x -> string x
    | AVar x -> string x
    | Add(e1, e2) -> "(" + fmt e1 + " + " + fmt e2 + ")"
    | Sub(e1, e2) -> "(" + fmt e1 + " - " + fmt e2 + ")"
    | Mul(e1, e2) -> "(" + fmt e1 + " * " + fmt e2 + ")"

// (iv)
let rec simplify (e: aexpr) : aexpr =
    match e with
    | Const x -> Const x
    | Add(e1, e2) ->
        match (e1, e2) with
        | Const 0, e2 -> e2
        | e1, Const 0 -> e1
        | e1, e2 -> Add(simplify e1, simplify e2)
    | Sub(e1, e2) ->
        match (e1, e2) with
        | e1, Const 0 -> e1
        | e1, e2 when e1 = e2 -> Const 0
        | e1, e2 -> Sub(simplify e1, simplify e2)
    | Mul(e1, e2) ->
        match (e1, e2) with
        | Const 1, e2 -> e2
        | e1, Const 1 -> e1
        | _, Const 0 -> Const 0
        | Const 0, _ -> Const 0
        | e1, e2 -> Mul(simplify e1, simplify e2)
// (v)
let rec diff (exp: aexpr) var : aexpr =
    match exp with
    | Const _ -> Const 0
    | AVar x -> if x = var then Const 1 else Const 0
    | Add(e1, e2) -> Add(diff e1 var, diff e2 var)
    | Sub(e1, e2) -> Sub(diff e1 var, diff e2 var)
    | Mul(e1, e2) -> Mul(diff e1 var, diff e2 var)