// micro-C example 3

void main(int n) { 
  int i; 
  i=0; 
  while (i < n) { 
    print i; 
    ++i;
  }

  int m;
  m = (n < 2) ? 66 : 10;
  print m;
}
