package tests_yandex;//hasNext()
//next()

import java.util.List;

//Реализовать итератор, который возвращает элементы, делящиеся на два.
//  a % 2 == 0
public class Iterator {
    private List<Integer> list;
    private Integer index = 0;

    public Iterator(List<Integer> list) {
        this.list = list;
    }

    public Boolean hasNext() {

        for (Integer x = index;  x < list.size(); x = x++)
       {
           if (x % 2 == 0)
           {
               index = x;
               return true;
           }
        }
        return false;
    }

    public Integer next() {
        for (Integer x = index;  x < list.size(); x = x++) {
            if (x % 2 == 0)
            {
                index = x;
                return x;}
        }
        return null;
    }
}
