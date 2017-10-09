package tests_yandex;

public final class RtriangleProvider {
    public static Rtriangle getRtriangle() {
        return new Rtriangle() {
            @Override
            public int getApexX1() {
                return 1;
            }

            @Override
            public int getApexY1() {
                return 100;
            }

            @Override
            public int getApexX2() {
                return 1;
            }

            @Override
            public int getApexY2() {
                return 1;
            }

            @Override
            public int getApexX3() {
                return 100;
            }

            @Override
            public int getApexY3() {
                return 1;
            }
        };
    }
}