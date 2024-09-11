(* Evaluation, checking, and compilation of object language expressions *)
(* Stack machines for expression evaluation                             *) 

(* Object language expressions with variable bindings and nested scope *)

module Intcomp1

let env = [ ("x", 1); ("a", 3); ("c", 78); ("baf", 666); ("b", 111) ]

///////////////////////////
///                     ///
///     EXERCISE 2.1    ///
///                     ///
///////////////////////////
type expr = 
  | CstI of int
  | Var of string
  | Let of (string * expr) list * expr
  | Prim of string * expr * expr;;

(* Some closed expressions: *)

let e1 = Let(["z", CstI 17], Prim("+", Var "z", Var "z"));;
let e2 = Let(["z", CstI 17], Prim("+", Let(["z", CstI 22], Prim("*", CstI 100, Var "z")),Var "z"));;
let e3 = Let(["z", Prim("-", CstI 5, CstI 4)], Prim("*", CstI 100, Var "z"));;
let e4 = Prim("+", Prim("+", CstI 20, Let(["z", CstI 17], Prim("+", Var "z", CstI 2))), CstI 30);;
let e5 = Prim("*", CstI 2, Let(["x", CstI 3], Prim("+", Var "x", CstI 4)));;
let e6 = Let(["z", Var "x"], Prim("+", Var "z", Var "x"));;
let e7 = Let(["z", CstI 3], Let(["y", Prim("+", Var "z", CstI 1)], Prim("+", Var "z", Var "y")))
let e8 = Let(["z", Let(["x", CstI 4], Prim("+", Var "x", CstI 5))], Prim("*", Var "z", CstI 2))
let e9 = Let(["z", CstI 3], Let(["y", Prim("+", Var "z", CstI 1)], Prim("+", Var "x", Var "y")))
let e10 = Let(["z", Prim("+", Let(["x", CstI 4], Prim("+", Var "x", CstI 5)), Var "x")], Prim("*", Var "z", CstI 2));;

(* ---------------------------------------------------------------------- *)

(* Evaluation of expressions with variables and bindings *)

let rec lookup env x =
    match env with 
    | []        -> failwith (x + " not found")
    | (y, v)::r -> if x=y then v else lookup r x;;

// Evaluates the body of a sequential let-binding expression.
// The statement:
//
//          " let x1 = 5+7 x2 = x1*2 in x1+x2 end "
//
// Would look as follows as an expression:
//
//          " Let ([("x1", ...); ("x2", ...)], Prim("+", Var "x1", Var "x2")) "
//
let rec eval (e : expr) (env : (string * int) list) : int =
    printf " - Environment State: %A\n\n" env   // For debugging / Educational purposes...
    
    match e with
    | CstI i            -> i
    | Var x             -> lookup env x
    | Prim("+", e1, e2) -> eval e1 env + eval e2 env
    | Prim("*", e1, e2) -> eval e1 env * eval e2 env
    | Prim("-", e1, e2) -> eval e1 env - eval e2 env
    | Let(bindings, expr) -> 
        // bindings: The bound variables - we may have multiple
        // exprs: The expression operation. No plural - only one. It is evaluated recursively.
    
        let rec aux bLst env =
            match bLst with
            | [] -> eval expr env
            | (x, erhs)::xs ->                  // 'x' is the variable binding - 'erhs' is the expression on the right hand side.
                let xVal   = eval erhs env      // We evaluate the 'erhs' expression.
                let newEnv = (x, xVal)::env     // We update the environment with the new value.
                aux xs newEnv
        aux bindings env
            
        
    | Prim _            -> failwith "unknown primitive";;
    
    
///////////////////////////
///                     ///
///     EXERCISE 2.2    ///
///                     ///
///////////////////////////
    
(* let mem x vs = List.exists (fun y -> x=y) vs;; *)
let rec mem x vs = 
    match vs with
    | []      -> false
    | v :: vr -> x=v || mem x vr;;
    
(* union(xs, ys) is the set of all elements in xs or ys, without duplicates *)
let rec union (xs, ys) = 
    match xs with 
    | []    -> ys
    | x::xr -> if mem x ys then union(xr, ys)
               else x :: union(xr, ys);;
             
(* minus xs ys  is the set of all elements in xs but not in ys *)  
let rec minus (xs, ys) = 
    match xs with 
    | []    -> []
    | x::xr -> if mem x ys then minus(xr, ys)
               else x :: minus (xr, ys);;
               
let rec freevars (e : expr) : string list =
    match e with
    | CstI _ -> []
    | Var  x  -> [x]
    | Let(bindings, expr) -> 
          match bindings with
          | [] -> freevars expr
          | (x1, x2)::xs ->
              union (freevars x2, minus (freevars (Let(xs, expr)), [x1]))
    | Prim(_, e1, e2) -> union (freevars e1, freevars e2);;
    
    
///////////////////////////
///                     ///
///     EXERCISE 2.3    ///
///                     ///
///////////////////////////
// This type is used to allow replacement of variable names into variable addresses (integer)
// This is closer to actual compilers which use hexidecimals as their address (numbers).
type texpr =                              (* target expressions *)
    | TCstI of int
    | TVar of int                         (* index into runtime environment *)
    | TLet of texpr * texpr               (* erhs and ebody                 *)
    | TPrim of string * texpr * texpr;;
    
(* Map variable name to variable index at compile-time *)
let rec getindex vs x = 
    match vs with 
    | []    -> failwith "Variable not found"
    | y::yr -> if x=y then 0 else 1 + getindex yr x;;
    
// Variable 'cenv' is the compile-time environment. In the 'tcomp' function it is just a string list representing the bound variables.
// Variable 'renv' is the run-time environment which stores the values of the variables.
// Both 'cenv' and 'renv' are directly mapped via their indices, so each variable will correspond to its intended value.
// Function 'tcomp' is used to convert an expression 'expr' into a 'texpr'
let rec tcomp (e : expr) (cenv : string list) : texpr =
    match e with
    | CstI i -> TCstI i
    | Var x  -> TVar (getindex cenv x)
    | Let(binding, expr) -> 
        match binding with
        | [] -> tcomp expr cenv
        | (bVar, varExpr)::_ ->         // bVar = bound variable && varExpr = variable expression
            let newCenv = bVar :: cenv
            TLet (tcomp varExpr newCenv, tcomp expr newCenv)
    | Prim(ope, e1, e2) -> TPrim(ope, tcomp e1 cenv, tcomp e2 cenv);;
    
    
(************************

ASSIGNMENT 2

************************)
    

    
