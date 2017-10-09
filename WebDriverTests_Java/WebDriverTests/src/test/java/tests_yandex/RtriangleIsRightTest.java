package tests_yandex;

import org.junit.Assert;
import org.testng.annotations.BeforeTest;
import org.testng.annotations.Test;
import ru.yandex.qatools.allure.annotations.Description;
import ru.yandex.qatools.allure.annotations.Title;

import java.util.Arrays;
import java.util.List;

//Все решение предоставлено для малых чисел (не больше int). Если нужно, перепишу для больших
@Title("Тесты на проверку прямоугольного треугольника")
public class RtriangleIsRightTest
{
    List<Integer> list = Arrays.asList(1, 2, 3, 4);
    Boolean f = new Iterator(list).hasNext();
    private Rtriangle rtriangle = RtriangleProvider.getRtriangle();
    private int x1 = rtriangle.getApexX1();
    private int x2 = rtriangle.getApexX2();
    private int x3 = rtriangle.getApexX3();
    private int y1 = rtriangle.getApexY1();
    private int y2 = rtriangle.getApexY2();
    private int y3 = rtriangle.getApexY3();

    @BeforeTest
    @Description("проверяем, что это треугольник")
    public void RtriangleIsAndleTest() {
//Если прощадь треугольника не равна 0 тогда это треугольник S=1/2((х1-х3)(у2-у3)-(х2-х3)(у1-у3))
        System.out.print("Amigo");
        System.out.println("The");
        System.out.print("Best");
        int areaTriangle = (x1 - x3)*(y2 - y3) - (x2 - x3)*(y1 - y3);
        Assert.assertNotSame("It is not a triangle", areaTriangle, 0);
    }

    @Test
    @Description("проверяем, что это правильный треугольник")
    public void RtriangleIsRightTest() {
//Вычесляем квадраты стороны
        int length1 = (x2 - x1)*(x2 - x1) + (y2 - y1)*(y2 - y1);
        int length2 = (x3 - x2)*(x3 - x2) + (y3 - y2)*(y3 - y2);
        int length3 = (x1 - x3)*(x1 - x3) + (y1 - y3)*(y1 - y3);

//Проверяем теорему пифагора
        if (
                !(length1 == length2 + length3)
                && !(length2 == length1 + length3)
                && !(length3 == length1 + length2))
            Assert.fail("It is not a right triangle");
    }
}