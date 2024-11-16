module Continuation_Examples

// Original:
let rec len xs =
    match xs with
    | [] -> 0
    | x::xr -> 1 + len xr;;

// Continuation:
let lenc lst = 
    let rec aux l cF = 
        match l with
        | [] -> cF 0
        | _::xs -> aux xs (fun v -> cF(1 + v))

    aux lst id
    

// Accumulator Function:
let leni lst =
    let rec aux l acc =
        match l with
        | [] -> acc
        | _::xs -> aux xs (acc + 1)
    
    aux lst 0
    
    
    
// LIST REVERSAL:

// Original:
let rec rev xs =
    match xs with
    | [] -> []
    | x::xr -> rev xr @ [x];;
    
    
// CPS:


let revc lst =
    let rec aux l cF =
        match l with
        | []    -> cF []
        | x::xs -> aux xs (fun v -> cF (v @ [x]))
    
    aux lst id
    
    
let revi lst =
    let rec aux l acc =
        match l with
        | []    -> acc
        | x::xs -> aux xs (x :: acc)
    
    aux lst []
    
// Original: 
let rec prod xs =
    match xs with
    | [] -> 1
    | x::xr -> x * prod xr;;
    
    
let prodc lst =
    let rec aux l cF =
        match l with
        | []    -> cF 1
        | x::_ when x = 0 -> 0
        | x::xs -> aux xs (fun v -> cF (v * x))
        
    aux lst id
    
    
let prodi lst =
    let rec aux l acc =
        match l with
        | []    -> acc
        | x::_ when x = 0 -> 0
        | x::xs -> aux xs (acc * x)
    
    aux lst 1
    
    
