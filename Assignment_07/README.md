# Assignment 8

## Exercise 8.1 

#### Question:

**_Study the generated symbolic bytecode. Write up the bytecode in a more struc-
tured way with labels only at the beginning of the line (as in this chapter). Write
the corresponding micro-C code to the right of the stack machine code. Note that
ex5.c has a nested scope (a block { ... }inside a function body); how is that
visible in the generated code?
Execute the compiled prog_**

#### Answer

```fsharp
  [LDARGS; CALL (1, "L1"); STOP; Label "L1"; INCSP 1; GETBP; CSTI 1; ADD;
   CSTI 0; STI; INCSP -1; GOTO "L3"; Label "L2"; GETBP; CSTI 1; ADD; LDI;
   PRINTI; INCSP -1; GETBP; CSTI 1; ADD; GETBP; CSTI 1; ADD; LDI; CSTI 1; ADD;
   STI; INCSP -1; INCSP 0; Label "L3"; GETBP; CSTI 1; ADD; LDI; GETBP; CSTI 0;
   ADD; LDI; LT; IFNZRO "L2"; INCSP -1; RET 0]
```

Here, we are trying to make the symbolic bytecode a bit more structured:
```fsharp
    [LDARGS; 
        CALL (1, "L1"); 
        STOP; 
        Label "L1"; 
            INCSP 1; //increment stack pointer, i.e. make space for operation(s)
            GETBP; CSTI 1; ADD; //gets addr of a variable, i,
            CSTI 0; //gets integer n.
            STI; //store THAT in memory!
            INCSP -1; //pop EVERYTHING
            GOTO "L3"; //Jump to L3
        Label "L2"; 
            GETBP; CSTI 1; ADD; //Gets variable i
            LDI; //load variable i.
            PRINTI; //print i
            INCSP -1; //pop all the shit.
            GETBP; CSTI 1; ADD; //load variable i 
            GETBP; CSTI 1; ADD; //load variable i cuz i ? i+1
            LDI; //Load that IN! 
            CSTI 1; ADD; //make the i+1
            STI; //Store THAT in memory BABYYY
            INCSP -1; //Pop all the shit! 
            INCSP 0; //what does this do? Doesn't return anything.
        Label "L3"; 
            GETBP; CSTI 1; ADD; //gets addr of variable i.
            LDI;  //load that variable, i, in.
            GETBP; CSTI 0; ADD; //gets addr of variable n
            LDI; //load THAT variable, n, in.
            LT;  //Less than, returns 1 for true, 0 for false
            IFNZRO "L2"; //if it IS less than, jump to L3
            INCSP -1; //Pop all of the shit.
    RET 0]
```

```C++
// micro-C example 3

void main(int n) { //L1
  int i;           //L1
  i=0;             //L1
  while (i < n) {  //L3
    print i;       //L2
    i=i+1;         //L2
  } 
}
```


