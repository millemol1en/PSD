void main(int n)
{
    int arr[7];     // according to exercises
    int freq[4];    // according to exercises

    freq[0] = 0;
    freq[1] = 0;
    freq[2] = 0;
    freq[3] = 0;
    
    arr[0] = 1;
    arr[1] = 2;
    arr[2] = 1;
    arr[3] = 1;
    arr[4] = 1; 
    arr[5] = 2; 
    arr[6] = 0;

    histogram(7, arr, 3, freq);

    int i;
    i=0;

    while (i < 4)
    {
        print freq[i];
        i=i+1;
    }
}

void histogram(int n, int ns[], int max, int freq[])
{
    int currIdx;
    int currElem;
    int i;
    i=0;

    while(i < n)
    {
        currIdx = ns[i];
        currElem = freq[currIdx];
        freq[currIdx] = currElem + 1;
        
        i=i+1;
    }
}