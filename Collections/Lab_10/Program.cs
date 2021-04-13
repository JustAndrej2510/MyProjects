using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_10
{
    class WebResource
    {
        public string name { get; set; }
        private string domain;
        public string Domain
        {
            get
            {
                return domain;
            }
            set
            {
                bool point = false;
                for (int i = 0; i < value.Length; i++)
                {
                    if (value[i] == '.' && value[value.Length - 1] != '.')
                    {
                        point = true;
                        break;
                    }

                }
                if (point == true)
                    domain = value;
                else
                    throw new ArgumentException("Неправильное имя домена");
            }
        }

        public WebResource(string name, string domain)
        {
            this.name = name;
            this.Domain = domain;
        }
        public WebResource() { }
    }

    class ListOfWebResources<T>:IList<T>
    {
        public T[] webPages = new T[10];
        private int count = 0;

        //Индексатор
        public T this[int i]
        {
            get
            {
                return webPages[i];
            }
            set
            {
                webPages[i] = value;
            }
        }

        //Проверка: список только для чтения или нет
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        //Счетчик
        public int Count
        {
            get
            {
                return count;
            }
        }

        //Конструкторы
        public ListOfWebResources(T resource)
        {
            this.Add(resource);
        }

        public ListOfWebResources() { }

        //Добавление
        public void Add(T res)
        {
            if (count < webPages.Length)
            {
                webPages[count] = res;
                count++;
            }
            //else
            //    Console.WriteLine("Список переполнен");
        }

        //Очистка
        public void Clear()
        {
            webPages = null;
            webPages = new T[10];
            count = 0;
        }

        //Поиск индекса элемента
        public int IndexOf(T res)
        { 
            for(int i=0; i<count;i++)
            {
                if(webPages[i].Equals(res))
                {
                    return i;
                }
            }
            return -1;
        }
        //Удаление по индексу
        public void RemoveAt(int index)
        {
            if (index >= 0 && index > count)
            {
                for (int j = index; j < webPages.Length - 1; j++)
                {
                    webPages[j] = webPages[j + 1];
                }
                count--;

            }
            else
                Console.WriteLine("Такого индекса нет в списке");
        }
        //Удаление элемента
        public bool Remove(T res)
        {
            int i = IndexOf(res);
            if (i != -1)
            {
                for(int j = i; j<webPages.Length-1; j++)
                {
                    webPages[j] = webPages[j+1];
                }
                count--;
                return true;
            }
            else
                return false;

        }
        // Проверяет: находиться ли элемент в списке
        public bool Contains(T res)
        {
            for (int i = 0; i < count; i++)
            {
                if (webPages[i].Equals(res))
                {
                    return true;
                }
            }
           return false;
        }
        //Добавляет элемент по определенному индексу
        public void Insert(int index, T res)
        {
            if(count + 1 <= webPages.Length && index < count && index >= 0)
            {
                for(int i = count-1; i > index; i--)
                {
                    webPages[i] = webPages[i-1]; 
                }
                Add(webPages[count - 1]);
                webPages[index] = res;
            }
            if (index > count)
                Add(res);
        }
        //Копирует список в массив с определенной позиции
        public void CopyTo(T[] array, int index)
        {
            for(int i = 0; i < count; i++, index++)
            {
                array.SetValue(webPages[i], index);
            }
        }
        public override bool Equals(object obj)
        {
            if (this.GetType() != obj.GetType()) return false;
            T res = (T)obj;
            return (this.GetHashCode() == res.GetHashCode());
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException("Нельзя использовать в данном контексте");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException("Нельзя использовать в данном контексте");
        }

        
    }

    class Program
    {
        static void Main(string[] args)
        {
            //1
            WebResource page = new WebResource("bstu", ".fit");
            WebResource page2 = new WebResource("tre", ".gov");
            WebResource page3 = new WebResource("fsd", ".net");
            WebResource page4 = new WebResource("hey", ".by");
            WebResource page5 = new WebResource("fit", ".belstu.by");
            ListOfWebResources<WebResource> list = new ListOfWebResources<WebResource>();
            list.Add(page);
            list.Add(page2);
            list.Add(page3);
            list.Clear();
            list.Add(page4);
            list.Add(page5);
            list.Remove(page3);
            list.Insert(3, page3);

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine("Ссылка ресурса: www." + list.webPages[i].name + list.webPages[i].Domain);
            }
            //list.Clear();

            //2
            ConcurrentDictionary<int, string> dictionary = new ConcurrentDictionary<int, string>();
            dictionary.TryAdd(1, "Elon");
            dictionary.TryAdd(2, "Musk");
            dictionary.TryAdd(3, "BSTU");
            dictionary.TryAdd(4, "FIT");
            foreach (var i in dictionary)
            {
                Console.WriteLine($"Ключ:{i.Key} Значение: {i.Value}");
            }
            Console.WriteLine("--------------------------------------------");
            string[] temp = new string[10];
            for (int i = 1; i < 3; i++)
            {
                dictionary.TryRemove(i, out temp[i - 1]);
            }
            dictionary.AddOrUpdate(1, "Dmitri", (key, oldValue) => oldValue);
            dictionary[6] = "Last element";
            foreach (var i in dictionary)
            {
                Console.WriteLine($"Ключ:{i.Key} Значение: {i.Value}");
            }
            Console.WriteLine("------------------------------------------");
            Dictionary<int, string> dict = new Dictionary<int, string>();
            foreach (var i in dictionary)
            {
                dict.Add(i.Key, i.Value);
            }
            foreach (var i in dict)
            {
                Console.WriteLine($"Ключ:{i.Key} Значение: {i.Value}");
            }
            bool check = dict.ContainsValue("FIT");
            if (check)
            {
                Console.WriteLine("Значение есть в словаре");
            }
            else
                Console.WriteLine("Значение не находится в словаре");

            //3
            ObservableCollection<WebResource> resColl = new ObservableCollection<WebResource>();
            resColl.CollectionChanged += WebPages_CollectionChanged;
            resColl.Add(new WebResource("bstu", ".by"));
            resColl.RemoveAt(0);
            resColl.Clear();
        }
        private static void WebPages_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    WebResource newRes = e.NewItems[0] as WebResource;
                    Console.WriteLine("Добавлен интернет-ресурс: " + newRes.name + newRes.Domain);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    WebResource oldRes = e.OldItems[0] as WebResource;
                    Console.WriteLine("Удален интернет-ресурс: " + oldRes.name + oldRes.Domain);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    Console.WriteLine("Список очищен");
                    break;
                case NotifyCollectionChangedAction.Replace:
                    WebResource replacedRes = e.OldItems[0] as WebResource;
                    WebResource replacingRes = e.NewItems[0] as WebResource;

                    Console.WriteLine("Элемент " + replacedRes.name + replacedRes.Domain + " заменен на " + replacingRes.name + replacingRes.Domain);
                    break;
            }
        }
    }
}
