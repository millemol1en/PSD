  .method public hidebysig static void SelectionSort(int32[] arr) cil managed // Define a public static method named SelectionSort that takes an integer array (arr) as input
{
  // Code size       81 (0x51)                        // The total code size is 81 bytes
  .maxstack  4                                        // Maximum stack depth is 4
  .locals init (int32 V_0,                            // Declare local variables:
               int32 V_1,                             // - V_0: index of current element
               int32 V_2,                             // - V_1: index of the minimum element
               int32 V_3,                             // - V_2: index for the inner loop
               bool V_4)                              // - V_3: temporary storage for element swapping
  IL_0000:  nop                                       // No operation (often used for debugging)
  IL_0001:  ldc.i4.0                                  // Load constant 0 onto the stack
  IL_0002:  stloc.0                                   // Store 0 in V_0 (start index of outer loop)
  IL_0003:  br.s       IL_0044                        // Branch to IL_0044 to start outer loop check

  IL_0005:  nop                                       // No operation
  IL_0006:  ldloc.0                                   // Load V_0 (current index) onto the stack
  IL_0007:  stloc.1                                   // Set V_1 (index of the smallest element) to V_0
  IL_0008:  ldloc.0                                   // Load V_0 onto the stack
  IL_0009:  ldc.i4.1                                  // Load constant 1 onto the stack
  IL_000a:  add                                       // Add 1 to V_0 (move to next element)
  IL_000b:  stloc.2                                   // Store result in V_2 (index for inner loop)
  IL_000c:  br.s       IL_0025                        // Branch to IL_0025 for inner loop condition

  IL_000e:  ldarg.0                                   // Load arr (method argument) onto the stack
  IL_000f:  ldloc.2                                   // Load V_2 (inner loop index) onto the stack
  IL_0010:  ldelem.i4                                 // Load the integer element at index V_2 from arr
  IL_0011:  ldarg.0                                   // Load arr onto the stack again
  IL_0012:  ldloc.1                                   // Load V_1 (index of current minimum) onto the stack
  IL_0013:  ldelem.i4                                 // Load the integer element at index V_1 from arr
  IL_0014:  clt                                       // Check if arr[V_2] < arr[V_1]
  IL_0016:  ldc.i4.0                                  // Load constant 0 onto the stack
  IL_0017:  ceq                                       // Compare result with 0 (invert result)
  IL_0019:  stloc.s    V_4                            // Store result in V_4 (boolean flag)
  IL_001b:  ldloc.s    V_4                            // Load V_4 onto the stack
  IL_001d:  brtrue.s   IL_0021                        // If V_4 is true, branch to IL_0021

  IL_001f:  ldloc.2                                   // Load V_2 onto the stack
  IL_0020:  stloc.1                                   // Set V_1 to V_2 (update minimum index)
  IL_0021:  ldloc.2                                   // Load V_2 onto the stack
  IL_0022:  ldc.i4.1                                  // Load constant 1 onto the stack
  IL_0023:  add                                       // Increment V_2
  IL_0024:  stloc.2                                   // Store result in V_2
  IL_0025:  ldloc.2                                   // Load V_2 onto the stack
  IL_0026:  ldarg.0                                   // Load arr onto the stack
  IL_0027:  ldlen                                     // Get the length of arr
  IL_0028:  conv.i4                                   // Convert length to int32
  IL_0029:  clt                                       // Compare V_2 with length of arr
  IL_002b:  stloc.s    V_4                            // Store result in V_4
  IL_002d:  ldloc.s    V_4                            // Load V_4 onto the stack
  IL_002f:  brtrue.s   IL_000e                        // If V_4 is true, continue inner loop

  IL_0031:  ldarg.0                                   // Load arr onto the stack
  IL_0032:  ldloc.0                                   // Load V_0 onto the stack
  IL_0033:  ldelem.i4                                 // Load the element at index V_0 from arr
  IL_0034:  stloc.3                                   // Store it in V_3 (temporary variable for swapping)
  IL_0035:  ldarg.0                                   // Load arr onto the stack
  IL_0036:  ldloc.0                                   // Load V_0 onto the stack
  IL_0037:  ldarg.0                                   // Load arr onto the stack again
  IL_0038:  ldloc.1                                   // Load V_1 onto the stack
  IL_0039:  ldelem.i4                                 // Load the element at index V_1 from arr
  IL_003a:  stelem.i4                                 // Store it at index V_0 (swap arr[V_0] with arr[V_1])
  IL_003b:  ldarg.0                                   // Load arr onto the stack
  IL_003c:  ldloc.1                                   // Load V_1 onto the stack
  IL_003d:  ldloc.3                                   // Load V_3 (original arr[V_0]) onto the stack
  IL_003e:  stelem.i4                                 // Store it at index V_1 (complete swap)
  IL_003f:  nop                                       // No operation
  IL_0040:  ldloc.0                                   // Load V_0 onto the stack
  IL_0041:  ldc.i4.1                                  // Load constant 1 onto the stack
  IL_0042:  add                                       // Increment V_0 (move to next position in outer loop)
  IL_0043:  stloc.0                                   // Store result in V_0
  IL_0044:  ldloc.0                                   // Load V_0 onto the stack
  IL_0045:  ldarg.0                                   // Load arr onto the stack
  IL_0046:  ldlen                                     // Get the length of arr
  IL_0047:  conv.i4                                   // Convert length to int32
  IL_0048:  clt                                       // Compare V_0 with length of arr
  IL_004a:  stloc.s    V_4                            // Store result in V_4
  IL_004c:  ldloc.s    V_4                            // Load V_4 onto the stack
  IL_004e:  brtrue.s   IL_0005                        // If V_4 is true, continue outer loop

  IL_0050:  ret                                       // Return from the method
} // end of method SelectionSort
