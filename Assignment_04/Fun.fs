(* File Fun/Fun.fs
   A strict functional language with integers and first-order 
   one-argument functions * sestoft@itu.dk

   Does not support mutually recursive function bindings.

   Performs tail recursion in constant space (because F# does).
*)

module Fun

open Absyn
open Parse

(* Environment operations *)
// NOTE: The 'v is a generic type
type 'v env = (string * 'v) list

let rec lookup env x =
    match env with 
    | []        -> failwith (x + " not found")
    | (y, v)::r -> if x=y then v else lookup r x;;

(* A runtime value is an integer or a function closure *)

type value = 
  | Int of int
  | Closure of string * string list * expr * value env       (* (f, x, fBody, fDeclEnv) *)

let rec eval (e : expr) (env : value env) : int =
    match e with 
    | CstI i -> i
    | CstB b -> if b then 1 else 0
    | Var x  ->
      match lookup env x with
      | Int i -> i 
      | _     -> failwith "eval Var"
    | Prim(ope, e1, e2) -> 
      let i1 = eval e1 env
      let i2 = eval e2 env
      match ope with
      | "*" -> i1 * i2
      | "+" -> i1 + i2
      | "-" -> i1 - i2
      | "=" -> if i1 = i2 then 1 else 0
      | "<" -> if i1 < i2 then 1 else 0
      | _   -> failwith ("unknown primitive " + ope)
    | Let(x, eRhs, letBody) -> 
      let xVal = Int(eval eRhs env)
      let bodyEnv = (x, xVal) :: env
      eval letBody bodyEnv
    | If(e1, e2, e3) -> 
      let b = eval e1 env
      if b<>0 then eval e2 env
      else eval e3 env
    | Letfun(f, x, fBody, letBody) -> 
      let bodyEnv = (f, Closure(f, x, fBody, env)) :: env 
      eval letBody bodyEnv
    | Call(Var f, eArg) ->
      // A function call Call(Var f, eArg) is evaluated by first checking that f is bound to a function closure
      // Closure (f, x, fBody, fDeclEnv). Then the argument expression eArg is evaluated to obtain an argument
      // value xVal
      
      let fClosure = lookup env f
      match fClosure with
      | Closure (f, argLst, fBody, fDeclEnv) ->
        // 01. We use 'List.zip' to fix together the "eArg" argument expressions 
        let boundArgs = (List.zip argLst eArg)
        let argValues = List.map(fun (argName, argExpr) -> (argName, Int(eval argExpr env))) boundArgs
        // We evaluate the argument 'eArg' within the functions scope, here specified by the "Closure()" type.
        let fBodyEnv =  argValues @ (f, fClosure) :: fDeclEnv
        eval fBody fBodyEnv
      | _ -> failwith "eval Call: not a function"
    | Call _ -> failwith "eval Call: not first-order function"

(* Evaluate in empty environment: program must have no free variables: *)

let run e = eval e [];;

(* Examples are now in FunOld.txt *)

////////////////////////
//                    //
//    EXERCISE 4.2    //
//                    //
////////////////////////

(*

Exercise 4.2 Write more example programs in the functional language, and test
them:
  • Compute the sum of the numbers from 1000 down to 1. Do this by defining a
    function sum n that computes the sum n + (n − 1) +···+ 2 + 1. (Use straightforward summation, no clever tricks.)
  • Compute the number 38, that is, 3 raised to the power 8. Again, use a recursive
    function.
  • Compute 30 + 31 +···+ 310 + 311, using a recursive function (or two, if you
    prefer).
  • Compute 18 + 28 +···+ 108, again using a recursive function (or two).

*)

// Question 4.2.1:
let Question4_2_1_String = @"
  let sum n = if n=0 then 0 else n + sum(n-1)
  in sum 1000 end
";;

// let q4_2_1 = 
//     Letfun("sum", "n", If
//        (Prim ("=", Var "n", CstI 0), CstI 0,
//         Prim ("+", Var "n", Call (Var "sum", Prim ("-", Var "n", CstI 1)))),
//     Call (Var "sum", CstI 1000))

// Question 4.2.2:
let Question4_2_2_String = @"
  let pow n = if n = 0 then 1 else 3 * pow (n - 1)
  in pow 8 end
";;

// let Question4_2_2_Expr =
//     Letfun ("pow", "n",
//         If
//        (Prim ("=", Var "n", CstI 0), CstI 1,
//         Prim ("*", CstI 3, Call (Var "pow", Prim ("-", Var "n", CstI 1)))),
//      Call (Var "pow", CstI 8))

// Question 4.2.3:
let Question4_2_3_String = @"
  let pow n = if n = 0 then 1 else 3 * pow (n - 1)
  in let sum n = if n=0 then 1 else (pow n) + sum(n - 1)
    in sum 11
    end
  end
";;

// let Question4_2_3_Expr =
//     Letfun ("pow", "n",
//      If
//        (Prim ("=", Var "n", CstI 0), CstI 1,
//         Prim ("*", CstI 3, Call (Var "pow", Prim ("-", Var "n", CstI 1)))),
//      Letfun
//        ("sum", "n",
//         If
//           (Prim ("=", Var "n", CstI 0), CstI 1,
//            Prim
//              ("+", Call (Var "pow", Var "n"),
//               Call (Var "sum", Prim ("-", Var "n", CstI 1)))),
//         Call (Var "sum", CstI 11)));;


// Question 4.2.4:
let Question4_2_4_String = @"
  let sum8Time n = n*n*n*n*n*n*n*n
  in let sum n = if n = 1 then 0 else (sum8Time n) + sum(n - 1)
    in sum 10
    end
  end
";;

////////////////////////
//                    //
//    EXERCISE 4.3    //
//                    //
////////////////////////
