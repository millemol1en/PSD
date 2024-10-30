## Exercise 8.1:

### i)
```fsharp
compileToFile (fromFile "./Examples/ex3.c") "./Examples/ex3.out" ;;  
val it: Machine.instr list =
  [LDARGS; CALL (1, "L1"); STOP; Label "L1"; INCSP 1; GETBP; CSTI 1; ADD;
   CSTI 0; STI; INCSP -1; GOTO "L3"; Label "L2"; GETBP; CSTI 1; ADD; LDI;
   PRINTI; INCSP -1; GETBP; CSTI 1; ADD; GETBP; CSTI 1; ADD; LDI; CSTI 1; ADD;
   STI; INCSP -1; INCSP 0; Label "L3"; GETBP; CSTI 1; ADD; LDI; GETBP; CSTI 0;
   ADD; LDI; LT; IFNZRO "L2"; INCSP -1; RET 0]
   

gcc -o machine machine.c
./machine ex11.out 8 

1 5 8 6 3 7 2 4 
1 6 8 3 7 4 2 5 
1 7 4 6 8 2 5 3 
1 7 5 8 2 4 6 3 
2 4 6 8 3 1 7 5 
2 5 7 1 3 8 6 4 
2 5 7 4 1 8 6 3 
2 6 1 7 4 8 3 5 
2 6 8 3 1 4 7 5 
2 7 3 6 8 5 1 4 
2 7 5 8 1 4 6 3 
2 8 6 1 3 5 7 4 
3 1 7 5 8 2 4 6 
3 5 2 8 1 7 4 6 
3 5 2 8 6 4 7 1 
3 5 7 1 4 2 8 6 
3 5 8 4 1 7 2 6 
3 6 2 5 8 1 7 4 
3 6 2 7 1 4 8 5 
3 6 2 7 5 1 8 4 
3 6 4 1 8 5 7 2 
3 6 4 2 8 5 7 1 
3 6 8 1 4 7 5 2 
3 6 8 1 5 7 2 4 
3 6 8 2 4 1 7 5 
3 7 2 8 5 1 4 6 
3 7 2 8 6 4 1 5 
3 8 4 7 1 6 2 5 
4 1 5 8 2 7 3 6 
4 1 5 8 6 3 7 2 
4 2 5 8 6 1 3 7 
4 2 7 3 6 8 1 5 
4 2 7 3 6 8 5 1 
4 2 7 5 1 8 6 3 
4 2 8 5 7 1 3 6 
4 2 8 6 1 3 5 7 
4 6 1 5 2 8 3 7 
4 6 8 2 7 1 3 5 
4 6 8 3 1 7 5 2 
4 7 1 8 5 2 6 3 
4 7 3 8 2 5 1 6 
4 7 5 2 6 1 3 8 
4 7 5 3 1 6 8 2 
4 8 1 3 6 2 7 5 
4 8 1 5 7 2 6 3 
4 8 5 3 1 7 2 6 
5 1 4 6 8 2 7 3 
5 1 8 4 2 7 3 6 
5 1 8 6 3 7 2 4 
5 2 4 6 8 3 1 7 
5 2 4 7 3 8 6 1 
5 2 6 1 7 4 8 3 
5 2 8 1 4 7 3 6 
5 3 1 6 8 2 4 7 
5 3 1 7 2 8 6 4 
5 3 8 4 7 1 6 2 
5 7 1 3 8 6 4 2 
5 7 1 4 2 8 6 3 
5 7 2 4 8 1 3 6 
5 7 2 6 3 1 4 8 
5 7 2 6 3 1 8 4 
5 7 4 1 3 8 6 2 
5 8 4 1 3 6 2 7 
5 8 4 1 7 2 6 3 
6 1 5 2 8 3 7 4 
6 2 7 1 3 5 8 4 
6 2 7 1 4 8 5 3 
6 3 1 7 5 8 2 4 
6 3 1 8 4 2 7 5 
6 3 1 8 5 2 4 7 
6 3 5 7 1 4 2 8 
6 3 5 8 1 4 2 7 
6 3 7 2 4 8 1 5 
6 3 7 2 8 5 1 4 
6 3 7 4 1 8 2 5 
6 4 1 5 8 2 7 3 
6 4 2 8 5 7 1 3 
6 4 7 1 3 5 2 8 
6 4 7 1 8 2 5 3 
6 8 2 4 1 7 5 3 
7 1 3 8 6 4 2 5 
7 2 4 1 8 5 3 6 
7 2 6 3 1 4 8 5 
7 3 1 6 8 5 2 4 
7 3 8 2 5 1 6 4 
7 4 2 5 8 1 3 6 
7 4 2 8 6 1 3 5 
7 5 3 1 6 8 2 4 
8 2 4 1 7 5 3 6 
8 2 5 3 1 7 4 6 
8 3 1 6 2 5 7 4 
8 4 1 3 6 2 7 5 

Used   0.010 cpu seconds
```

## ii)
### 'ex3.c':
#### Compilation:
```fsharp
> compileToFile (fromFile "./Examples/ex3.c") "./Examples/ex3.out";;  
val it: Machine.instr list =
  [LDARGS; CALL (1, "L1"); STOP; Label "L1"; INCSP 1; GETBP; CSTI 1; ADD;
   CSTI 0; STI; INCSP -1; GOTO "L3"; Label "L2"; GETBP; CSTI 1; ADD; LDI;
   PRINTI; INCSP -1; GETBP; CSTI 1; ADD; GETBP; CSTI 1; ADD; LDI; CSTI 1; ADD;
   STI; INCSP -1; INCSP 0; Label "L3"; GETBP; CSTI 1; ADD; LDI; GETBP; CSTI 0;
   ADD; LDI; LT; IFNZRO "L2"; INCSP -1; RET 0]


```

#### MicroC Code:
```c++
void main(int n) { 
  int i; 
  i=0; 
  while (i < n) { 
    print i; 
    i=i+1;
  } 
}
```

#### Bytecode:
```fsharp
24 19 1 5 25 15 1 13 0 1 1 0 0 12 15 -1 16 43 13 0 1 1 11 22 15 -1 13 0 1 1 13 0 1 1 11 0 1 1 12 15 -1 15 0 13 0 1 1 11 13 0 0 1 11 7 18 18 15 -1 21 0
```

#### Bytecode Analysis:
```fsharp
[
LDARGS;                     // Add the Command-line arguments to the stack
    CALL (1, "L1");         // The main function
    STOP;                   // Terminates the program
    Label "L1";             
        INCSP 1;            // Initiate the 'i'                     
        GETBP; CSTI 1; ADD; // Get the address of i
        CSTI 0;             // Set the 'i' to be equal to 0
        STI;                // Store the value in our environment
        INCSP -1;           // Clears the stack
        GOTO "L3";          // Jump to "L3" - the while loop
   Label "L2";          
        GETBP; CSTI 1; ADD; // Get the address of 'i'
        LDI;                // Load 'i' onto the stack
        PRINTI;             // Perform print of 'i'
        INCSP -1;           // Remove the copy of 'i' from the top of the stack
        GETBP; CSTI 1; ADD; // Loads one instance of the address at i - we need to for reassigning the new value of 'i'
        GETBP; CSTI 1; ADD; // Loads a second instance of the address of 'i' - we need this for the actual calculations.
        LDI;                // Load the second i onto the stack
        CSTI 1; ADD;        // Add 1 to the element on the top of the stack, in this case, the 2nd 'i'
        STI;                // Store the result on the stack
        INCSP -1;           // Remove the result of STI from the stack
        INCSP 0;            // No locals were created so removes zero elements.
   Label "L3";              //
        GETBP; CSTI 1; ADD; // Get location of 'i' 
        LDI;                // Loads 'i' onto the stack
        GETBP; CSTI 0; ADD; // Get location of 'n'
        LDI;                // Loads 'n' onto the stack
        LT;                 // Perform comparison between 'i' and 'n'
        IFNZRO "L2";        // Jumps to L2 if the result is NOT zero
        INCSP -1;           // Pop the top of the stack
        RET 0               // Default return 
]

```


### 'ex5.c':
#### Compilation:
```fsharp
> compileToFile (fromFile "./Examples/ex5.c") "./Examples/ex5.out";;
val it: Machine.instr list =
  [LDARGS; CALL (1, "L1"); STOP; Label "L1"; INCSP 1; GETBP; CSTI 1; ADD;
   GETBP; CSTI 0; ADD; LDI; STI; INCSP -1; INCSP 1; GETBP; CSTI 0; ADD; LDI;
   GETBP; CSTI 2; ADD; CALL (2, "L2"); INCSP -1; GETBP; CSTI 2; ADD; LDI;
   PRINTI; INCSP -1; INCSP -1; GETBP; CSTI 1; ADD; LDI; PRINTI; INCSP -1;
   INCSP -1; RET 0; Label "L2"; GETBP; CSTI 1; ADD; LDI; GETBP; CSTI 0; ADD;
   LDI; GETBP; CSTI 0; ADD; LDI; MUL; STI; INCSP -1; INCSP 0; RET 1]
```


#### MicroC Code:
```c++
void main(int n) {
  int r; 
  r = n;
  { 
    int r;
    square(n, &r);
    print r;
  }
  print r;
}

void square(int i, int *rp) {
  *rp = i * i;
}
```

#### Bytecode:
```fsharp
24 19 1 5 25 15 1 13 0 1 1 13 0 0 1 11 12 15 -1 15 1 13 0 0 1 11 13 0 2 1 19 2 57 15 -1 13 0 2 1 11 22 15 -1 15 -1 13 0 1 1 11 22 15 -1 15 -1 21 0 13 0 1 1 11 13 0 0 1 11 13 0 0 1 11 3 12 15 -1 15 0 21 1
```

#### Bytecode Analysis:
```fsharp
[
LDARGS; 
    CALL (1, "L1");         // Jump to L1, which is our "main" function
    STOP;                   // Halt when the function is done
    Label "L1"; 
        INCSP 1;            // Allocate space on stack for "r".
        GETBP; CSTI 1; ADD; // Get address of "r".
        GETBP; CSTI 0; ADD; // Get address of "n".
        LDI;                // Place "n" on to the stack.
        STI;                // Set "r" to be equal to "n".
        INCSP -1;           // Remove the result of STI from the stack.
        INCSP 1;            // Allocate space for the new "r" located within the inner block.
        GETBP; CSTI 0; ADD; // Get the address of "n" just like before.
        LDI;                // Load it onto the stack.
        GETBP; CSTI 2; ADD; // Get the address of the new inner "r". Note how we don't load it onto the stack yet because we only need the address.
        CALL (2, "L2");     // We call "L2" which is equal to the "square()" function. The 2 indicates the number of parameters!!!
        INCSP -1;           // Remove the return value of square() from the stack.
        GETBP; CSTI 2; ADD; // Get the address of the inner "r"
        LDI;                // Get the value of "r" and place it at the top of the stack.
        PRINTI;             // Print the top value of the stack - in this case "r".
        INCSP -1;           // Remove the inner "r" from print
        INCSP -1;           // Remove the inner "r" from the block
        GETBP; CSTI 1; ADD; // Get the address of the first "r" - the outer "r".
        LDI;                // Place it onto the stack.
        PRINTI;             // Print the value on top of the stack.
        INCSP -1;           // Remove the outer "r" from print
        INCSP -1;           // Remove the outer "r" from the block
        RET 0;              // Return from main and removes 0 parameters from the stack.
   Label "L2"; 
        GETBP; CSTI 1; ADD; // Get address of "*rp" from the function argument [does CSTI refer to the argument in this case?]
        LDI;                // Load it onto the stack
        GETBP; CSTI 0; ADD; // Get addres of 1st "i" to be used in "i * i" 
        LDI;                // Loads it onto the stack
        GETBP; CSTI 0; ADD; // Get addres of 2nd "i" to be used in "i * i" 
        LDI;                // Loads it onto the stack
        MUL;                // Perform multiplication of the 2 top values at the stack. 
        STI;                // Place the result of multiplication into the "*rp" as ---> "s, i, v" => "s[i] = v" where 'v' is "i * i" on the top of the stack and 'i' is "*rp", just below it.
        INCSP -1;           // Remove the value from STI from the stack.
        INCSP 0;            // No local variables initialized so no need to remove them
        RET 1               // Pops the return value from the stack 
]
```

## Exercise 8.4:
### File 'ex8.c'
#### 'prog1.out'
```fsharp
// ex8.out:
0 20000000 16 7 0 1 2 9 18 4 25

// Prettified ex8.out:
 0: CSTI 20000000 
16: GOTO 7 
 0: CSTI 1 
 2: SUB 
 9: DUP 
18: IFNZRO 4 
25: STOP
```

#### Symbolic Bytecode:
```fsharp
[LDARGS; 
  CALL (0, "L1"); 
  STOP; 
  Label "L1"; 
    INCSP 1; 
    GETBP; CSTI 0; ADD;
    CSTI 20000000; 
    STI; 
    INCSP -1; 
    GOTO "L3"; 
  Label "L2"; 
    GETBP; CSTI 0; ADD;
    GETBP; CSTI 0; ADD; LDI;
    CSTI 1; 
    SUB;
    STI;
    INCSP -1;
    INCSP 0;
  Label "L3";
    GETBP; CSTI 0; ADD; LDI; 
    IFNZRO "L2"; 
    INCSP -1; 
    RET -1
  ]
```

The `prog.c` ...

### File 'ex13.c'
##### Generated instructions from "ex13.c"
```fsharp
> compile "ex13";;
[LDARGS; 
    CALL (1, "L1"); 
    STOP; 
    Label "L1"; 
        INCSP 1; 
        GETBP; CSTI 1; ADD;
        CSTI 1889; 
        STI; 
        INCSP -1; 
        GOTO "L3"; 
    Label "L2"; 
        GETBP; CSTI 1; ADD; 
        GETBP; CSTI 1; ADD; LDI; 
        CSTI 1; ADD; 
        STI; 
        INCSP -1; 
        GETBP; CSTI 1; ADD; LDI;
        CSTI 4; 
        MOD; 
        CSTI 0; 
        EQ; 
        IFZERO "L7"; 
        GETBP; CSTI 1; ADD; LDI; 
        CSTI 100;
        MOD; 
        CSTI 0; 
        EQ; 
        NOT; 
        IFNZRO "L9"; 
        GETBP; CSTI 1; ADD;  LDI; 
        CSTI 400; 
        MOD;
        CSTI 0; 
        EQ; 
        GOTO "L8"; 
    Label "L9"; 
        CSTI 1; 
    Label "L8"; 
        GOTO "L6";
    Label "L7"; 
        CSTI 0; 
    Label "L6"; 
        IFZERO "L4"; 
        GETBP; CSTI 1; ADD; LDI;
        PRINTI; 
        INCSP -1; 
        GOTO "L5"; 
    Label "L4"; 
        INCSP 0; 
    Label "L5"; 
        INCSP 0;
    Label "L3"; 
        GETBP; CSTI 1; ADD; LDI; 
        GETBP; CSTI 0; ADD; LDI; 
        LT;
        IFNZRO "L2"; 
        INCSP -1; 
        RET 0
    ]
```

##### Results:
In MicroC, `while` loops and `if-else` statements are similar in the sense that they utilize the same
instruction types to jump around and alter the execution of code. Examples of these instructions include 
`IFNZRO` and `IFZERO` which are used to check for whether the condition in an if-statement and or while-loop 
holds. 



## Exercise 8.5:
The following MicroC example was run with the commands below: 
 - "java Machine ./Exercise/ex8_5.out 4" will give  10
 - "java Machine ./Exercise/ex8_5.out 21" will give 30
 - "java Machine ./Exercise/ex8_5.out 19" will give 20

```c++
void main(int input)
{
    int n;
    n = input < 10 ? 10 : input > 20 ? 30 : 20;
    print n;
}
```



## Exercise 8.6:
The following MicroC example was run with the commands below:
- "java Machine ./Exercise/ex8_6.out 0" will give 5
- "java Machine ./Exercise/ex8_6.out 1" will give 10
- "java Machine ./Exercise/ex8_6.out 2" will give 15

```c++
void main(int input)
{
    switch(input)
    {
        case 0:
            { input = 5; }
        case 1:
            { input = 10; }
        case 2:
            { input = 15; }
    }
    print input;
}
```

#### MicroC example: 