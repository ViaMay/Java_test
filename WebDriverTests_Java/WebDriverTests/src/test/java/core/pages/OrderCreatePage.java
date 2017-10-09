package core.pages;

import core.pagesControls.*;
import core.systemControls.TextInput;
import org.openqa.selenium.By;

public class OrderCreatePage extends HomePage
    {
        public OrderCreatePage()
        {
            setOrderLogistics(new OrderLogistics(By.xpath("//div[@class='panel logistics']")));
            setOrderPersonalInfo(new OrderPersonalInfo(By.xpath("//div[@class='panel personal-info third']")));
            setOrderCollectionInfo(new OrderCollectionInfo(By.xpath("//div[@class='panel collection-info']")));
            setOrderPayment(new OrderPayment(By.xpath("//div[@class='panel payment']")));

            setInfoModal(new InfoModal(By.xpath("//*[@data-name='modal.info']")));
     }

        @Override
        public void BrowseWaitVisible()
        {
            getOrderLogistics().WaitVisibleWithRetries();
            getOrderPersonalInfo().WaitVisibleWithRetries();
            getOrderCollectionInfo().WaitVisibleWithRetries();
            getOrderPayment().WaitVisibleWithRetries();
        }
        private core.pagesControls.OrderLogistics OrderLogistics;
        public final core.pagesControls.OrderLogistics getOrderLogistics()
        {
            return OrderLogistics;
        }
        public final void setOrderLogistics(core.pagesControls.OrderLogistics value)
        {
            OrderLogistics = value;
        }
        private OrderPersonalInfo OrderPersonalInfo;
        public final OrderPersonalInfo getOrderPersonalInfo()
        {
            return OrderPersonalInfo;
        }
        public final void setOrderPersonalInfo(OrderPersonalInfo value)
        {
            OrderPersonalInfo = value;
        }
        private OrderPayment OrderPayment;
        public final OrderPayment getOrderPayment()
        {
            return OrderPayment;
        }
        public final void setOrderPayment(OrderPayment value)
        {
            OrderPayment = value;
        }
        private OrderCollectionInfo OrderCollectionInfo;
        public final OrderCollectionInfo getOrderCollectionInfo()
        {
            return OrderCollectionInfo;
        }
        public final void setOrderCollectionInfo(OrderCollectionInfo value)
        {
            OrderCollectionInfo = value;
        }

        private core.pagesControls.InfoModal InfoModal;
        public InfoModal getInfoModal()
        {
            return InfoModal;
        }
        public void setInfoModal(InfoModal value)
        {
            InfoModal = value;
        }
    }
