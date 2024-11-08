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
Write 3-10 line descriptions of how the abstract machine executes each of the
following instructions:

```fsharp
ADD     // which adds two integers.
CSTI i  // which pushes integer constant i.
NIL     // which pushes a nil reference. What is the difference between NIL and CSTI 0?
IFZERO  // which tests whether an integer is zero, or a reference is nil.
CONS    
CAR
SETCAR
```

| Instruction | Result                                                                                                                                                                   |
|-------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `ADD`       | Untag the two top elements of the stack, and then add these two together. Tag them, and assign them to the stackpointer minus one and decrement the stackpointer by one. |
| `CSTI i`    |                                                                                                                                                                          |


### ii) [_for our group we assume it to be 32-bit_]
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

