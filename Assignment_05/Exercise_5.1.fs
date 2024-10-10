module PSD.Assignment_05.Exercise_5_1

//Exercise 5.1

let merge (xs : int list, ys : int list) : int list =
   let concat = xs @ ys
   List.sort concat
    