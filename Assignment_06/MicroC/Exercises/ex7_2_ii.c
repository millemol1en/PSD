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
    i=0;

    while(i < n)
    {
        arr[i] = i*i;
        i=i+1;
    }
}

void arrsum(int n, int arr[], int *sump)
{
    if (n == 0)
    {
        *sump = 0;
    } 
    int s;
    s=arr[0];
    
    int i;
    i=1;

    while(i < n)
    {
        s = s+arr[i];
        i=i+1; 
    }

    *sump = s;
}