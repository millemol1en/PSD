// Re-writing ex7.2.ii using for-loop instead of while:
void main()
{
    int arrSize  = 7;
    int freqSize = 3;
    
    int arr[arrSize];     // according to exercises
    int freq[freqSize];    // according to exercises

    // Population:
    int i;
    int j;
    int k;


    for(i=0; i < freqSize; i=i+1)
    {
        freq[i] = 0;
    }
    
    arr[0] = 1;
    arr[1] = 2;
    arr[2] = 1;
    arr[3] = 1;
    arr[4] = 1; 
    arr[5] = 2; 
    arr[6] = 0;

    histogram(arrSize, arr, freqSize, freq);
    
    for(k=0; k < 4; k=k+1)
    {
        print freq[i];
    }
}

void histogram(int n, int ns[], int max, int freq[])
{
    int currIdx;
    int currElem;
    int i;

    for (i = 0; i < n; ++i)
    {
        currIdx = ns[i];
        currElem = freq[currIdx];
        freq[currIdx] = currElem + 1;
    }
}