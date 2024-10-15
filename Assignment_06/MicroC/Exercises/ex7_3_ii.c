// Re-writing ex7.2.ii using for-loop instead of while:
void main(int n)
{
    int sump;
    int arr[20];

    squares(n, arr);
    arrsum(n, arr, &sump);

    print sump;
}

void squares(int n, int arr[])
{
    int i;
    
    for(i = 0; i < n; i=i+1)
    {
        arr[i] = i*i;
    }
}

void arrsum(int n, int arr[], int *sump)
{
    int s;
    s=arr[0];
    
    int i;
    
    for(i=1; i < n; i=i+1)
    {
        s = s+arr[i];
    }

    *sump = s;
}