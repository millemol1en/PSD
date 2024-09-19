```fsharp
Main ::= Expr EOF               // rule A
Expr ::= NAME                   // rule B
| CSTINT                        // rule C
| MINUS CSTINT                  // rule D
| LPAR Expr RPAR                // rule E
| LET NAME EQ Expr IN Expr END  // rule F
| Expr TIMES Expr               // rule G
| Expr PLUS Expr                // rule H
| Expr MINUS Expr               // rule I
```

Write out the rightmost derivation of the string:
```fsharp
  let z = (17) in z + 2 * 3 end EOF
```

Steps are:
1. `Expr EOF`                                                                            [rule A]
2. `LET NAME EQ Expr IN Expr END EOF`                                                    [rule F]
3. `LET NAME EQ Expr IN Expr PLUS Expr END EOF`                                          [rule H]
4. `LET NAME EQ Expr IN Expr PLUS Expr TIMES Expr END EOF`                               [rule G]
5. `LET NAME EQ Expr IN Expr PLUS Expr TIMES CSTINT(3) END EOF`                          [rule C]
6. `LET NAME EQ Expr IN Expr PLUS CSTINT(2) TIMES CSTINT(3) END EOF`                     [rule C]
7. `LET NAME EQ Expr IN NAME PLUS CSTINT(2) TIMES CSTINT(3) END EOF`                     [rule B]
8. `LET NAME EQ LPAR Expr RPAR IN NAME PLUS CSTINT(2) TIMES CSTINT(3) END EOF`           [rule E]
9. `LET NAME EQ LPAR CSTINT(17) RPAR IN NAME PLUS CSTINT(2) TIMES CSTINT(3) END EOF`     [rule C]
