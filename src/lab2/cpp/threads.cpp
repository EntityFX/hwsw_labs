#include <iostream>
#include <thread>
using namespace std;
//Простая функция
void foo(int Z)
{
  for (int i = 0; i < Z; i++) {
    cout << "Поток использует указатель на функцию"
      " как «вызываемую» - callable\n";
  }
}

// Вызываемый объект
class thread_obj {
public:
  void operator()(int x)
  {
    for (int i = 0; i < x; i++)
      cout << "Поток использует"
        " объект как вызываемый\n";
  }
}; 

int main()
{
  cout << "Потоки 1 и 2 выполняются независимо" << endl;
  // Данный поток выполняется используя функцию как «вызываемую»
  thread th1(foo, 3);
  // Данный поток выполняется используя ф-ый объект как «вызываемый»
  thread th2(thread_obj(), 3);
  // Ждем завершения потоков
  th1.join();
  th2.join();
  return 0;
} 