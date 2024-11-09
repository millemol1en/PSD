# Exercises 
## Exercise 10.1:
### Memory Structure:
A `cons` cell is a block which consists of three 32-bit words, namely:

#### The Heap:
The heap consists of 64-bit (8-byte) words. These


#### Block Header:
The block header `ttttttttnnnnnnnnnnnnnnnnnnnnnngg` that contains
8 tag bits (`t`), 22 length bits (`n`) and 2 garbage collection bits (`g`). For a cons
cell the tag bits will always be 00000000 and the length bits will be 00... 0010,
indicating that the cons cell has two words, not counting the header word. The
garbage collection bits gg will be interpreted as colors: `00` means white, `01` means
grey, `10` means black, and `11` means blue.

#### The "car" field:
A first field, called the `car` field, which can hold any abstract machine value. 

#### The "cdr" field:
A second field, called the `cdr` field, which can hold any abstract machine value.


### i)
**Write 3-10 line descriptions of how the abstract machine executes each of the following instructions:**

```fsharp
ADD     // which adds two integers.
CSTI i  // which pushes integer constant i.
NIL     // which pushes a nil reference. What is the difference between NIL and CSTI 0?
IFZERO  // which tests whether an integer is zero, or a reference is nil.
CONS    
CAR
SETCAR
```

Looking at the `listmachine.c` file, we can conclude the following:

| Instruction | Effect                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         |
|-------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `ADD`       | We first `Untag` the 2 values we are adding together and thereafter `Tag` their product. Finally, the result is stored in position `s[sp-1]` and the stack pointer `SP` is decrementing resulting in popping the top and making `s[sp-1]` the new top.                                                                                                                                                                                                                                                                                                                         |
| `CSTI i`    | We first `Tag` the value at `p[pc++]`, then place the result on stack `s[sp+1]` and finally we increment the `SP` by 1, giving us a new top on our stack.                                                                                                                                                                                                                                                                                                                                                                                                                      |
| `NIL`       | Whilst the instruction `CSTI i` would get marked, `NIL` does not. Instead, we simply place `0` at the position `s[sp+1]` and increment the `sp` by 1.                                                                                                                                                                                                                                                                                                                                                                                                                          |
| `IFZERO`    | We first pop the top of the stack (`s[sp--]`), giving us a `word` variable `v`. A ternary conditional checks if this variable `v` is an integer (`IsInt(v)`) which, in the case that it is, we need to first `Untag` it before checking to see if it is `0` or not. <br/><br/> Ultimately, the result from this is itself a ternary condition, which if the variable `v` was `0`, will set the `pc` to the next instruction `p[pc]` otherwise it will increment `pc` by `pc + 1`.                                                                                              |
| `CONS`      | We allocate space for 2 new objects on the heap - both of which have a of size of `word`. The allocate function returns a new pair `p`, representing this object. We then set the 1st element our pair `p` (_index 1_) to be equal to second top-most element `s[sp - 1]` and the 2nd element (_index 2_) to be set to the top-most element `s[sp]`. The indices equate to `CAR` and `CDR` respectively. <br/><br/> Finally, we set this new heap allocated pair `p` to be equal to second top-most position in the stack and pop the top-most leaving us with a new root `p`. |
| `CAR`       |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |
| `SETCAR`    |                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |


### ii) 
#### BlockTag(hdr) ((hdr)>>24)
Left and right bit shifting is performed as follows:
 - `0010 << 1  →  0100`  [LEFT]
 - `0010 << 2  →  1000`  [LEFT]
 - `1011 >> 3  →  0001`  [RIGHT]
 - `0101 >> 1  →  0010`  [RIGHT]

So, our 32-bit header is shifted by 24 places, resulting in us isolating the tag bits:

```fsharp
tttt tttt nnnn nnnn nnnn nnnn nnnn nngg >> 24 ->
0000 0000 0000 0000 0000 0000 tttt tttt
```

### Length(hdr) (((hdr)>>2)&0x003FFFFF)
In this macro, we first bit shift by 2, removing the last 2 bits used for coloring `gg`. Thereafter, we perform a `&` with the value `0x003FFFFF`, resulting in the isolation of the bits representing our block length `n`.

```fsharp
tttt tttt nnnn nnnn nnnn nnnn nnnn nngg >> 2 ->
00tt tttt ttnn nnnn nnnn nnnn nnnn nnnn 
&
0000 0000 0011 1111 1111 1111 1111 1111 ->
0000 0000 00nn nnnn nnnn nnnn nnnn nnnn
```

### Color(hdr) ((hdr)&3)
In this macro, we perform a basic `&` operand with our header, resulting in the isolation of the first 2 bits - which represents our color `g`.

```fsharp
tttt tttt nnnn nnnn nnnn nnnn nnnn nngg 
& 
0000 0000 0000 0000 0000 0000 0000 0011 ->
0000 0000 0000 0000 0000 0000 0000 00gg
```

### Paint(hdr, color) (((hdr)&(~3))\|(color))
The final instruction we first perform an `&` operand with a negated 0x3, resulting in the copying of the entire header except for the color:

```fsharp
tttt tttt nnnn nnnn nnnn nnnn nnnn nngg 
& 
1111 1111 1111 1111 1111 1111 1111 1100 // Bit representation of ~0x3
tttt tttt nnnn nnnn nnnn nnnn nnnn nn00
```

Thereafter, an `|` operand is performed to mash together the 2 byte sequences, resulting in us giving our header a new color [_as specified by the 2nd byte sequence_]

```fsharp
tttt tttt nnnn nnnn nnnn nnnn nnnn nn00 
| 
0000 0000 0000 0000 0000 0000 0000 00cc ->  // We might assume color to just be the first 2 bits `cc`
tttt tttt nnnn nnnn nnnn nnnn nnnn nncc
```

### iii) 
**When does the abstract machine, or more precisely, its instruction interpretation loop, call the `allocate` function? Is there any other interaction between the abstract machine (also called the mutator) and the garbage collector?**

Looking at the file `listmachine.c`, we see the function `allocate` is called inside the `switch-case` for `CONS`

```c++
case CONS: {
      word* p = allocate(CONSTAG, 2, s, sp);        // Allocate space for our object on the stack
      p[1] = (word)s[sp - 1];                       // Assign "car" to be the top-most value on our stack
      p[2] = (word)s[sp];                           // Assign "cdr" to be the 2nd top-most value on our stack
      s[sp - 1] = (word)p;                          // Now set the 2nd top-most position on our stack to be equal to our newly created object "p"
      sp--;                                         // Decrement the stack pointer - popping off the top but still keeping a root to link our object. 
    } break;
```


### iv)
**In what situation will the garbage collector’s `collect` function be called?**

In the case that there is no free space in the freelist, prompting the GC to perform garbage collection. This can be seen in the code snippet from `listmachine.c`:

```fsharp
// No free space, do a garbage collection and try again
    if (attempt == 1)
      collect(s, sp);
```
