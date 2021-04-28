import {
    HTTP
} from '../utils/http.js'
class Order extends HTTP {    
    url = 'Order.ashx'

    // 传入参数
    // params: { 
    //     data: { uid, cartIdList, payway, originPrice, discount, deliveryFee },
    //     success: (e) => {}
    // }
    submitOrder(params) {
        //console.log(params)
        this.request({
            url: this.url,
            data: {
                do: 'Submit',
                uid: params.data.uid,
                sid: params.data.sid,
                cartIdList: params.data.cartIdList,
                payway: params.data.payway,
                originPrice: params.data.originPrice,
                discount: params.data.discount,
                deliveryFee: params.data.deliveryFee,
                remark: params.data.remark
            },
            success: params.success
        })
    }

    // 传入参数
    // params: { 
    //     data: { uid, idlist },
    //     success: (e) => {}
    // }
    getPayData(params) {
        this.request({
            url: this.url,
            data: {
                do: 'GetPaymentData',
                uid: params.data.uid,
                idlist: params.data.idlist
            },
            success: params.success
        })
    }

    // 传入参数
    // params: { 
    //     data: { orderid },
    //     success: (e) => {}
    // }
    getOrderData(params) {
        this.request({
            url: this.url,
            data: {
                do: 'GetOrderData',
                orderid: params.data.orderid
            },
            success: params.success
        })
    }

    // 传入参数
    // params: { 
    //     data: { orderid, payway },
    //     success: (e) => {}
    // }
    pay(params) {
        this.request({
            url: this.url,
            data: {
                do: 'Pay',
                orderid: params.data.orderid,
                payway: params.data.payway
            },
            success: params.success
        })
    }

    // 传入参数
    // params: { 
    //     data: { uid, ispay },
    //     success: (e) => {}
    // }
    getCartList(params) {
        this.request({
            url: this.url,
            data: {
                do: 'GetOrderList',
                uid: params.data.uid,
                ispay: params.data.ispay
            },
            success: params.success
        })
    }
}
export{
    Order
}