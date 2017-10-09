package tests_yandex;

import org.testng.annotations.Test;

public class SolutionTest
{
    @Test
    public static void mainTest(String[] args)
    {
        //напишите тут ваш код
        Zerg[] zerg = new Zerg[10];
        for( int i = 0; i<10; i++)
        {
            zerg[i].name = "Zerg.name" + i;
        }

        Protos[] protos = new Protos[5];
        for( int i = 0; i<5; i++)
        {
            protos[i].name = "Protos.name" + i;
        }
        Terran[] terran = new Terran[5];
        for( int i = 0; i<5; i++)
        {
            terran[i].name = "Terran.name" + i;
        }
    }

    public static class Zerg
    {
        public String name;
    }

    public static class Protos
    {
        public String name;
    }

    public static class Terran
    {
        public String name;
    }
}