//app.js
import {
    HTTP
} from './common/utils/http.js'
import {
    Pro
} from './common/model/pro.js'
let http = new HTTP()
let pro = new Pro()
App({
    globalData: {
        userInfo: null,
        toolbar: null
    },
    request: function (params) {
        http.request(params)
    },
    getTabs: function (success) {
        pro.getNavbarData(success)
    },
    getCartCount: function (success) {
        pro.getCartCount({
            data: {
                uid: 'ding'
            },
            success: success
        })
    },
    onShow(options) {
        this.getTabs((e) => {
            this.globalData.toolbar = e.data.Tabs;
            //console.log(this.globalData.toolbar)
        })
        this.getCartCount((e) => {
            //console.log(e.data)
            this.globalData.toolbar[2].tag = e.data;
            //console.log(this.globalData.toolbar)
        })
    },
    onLaunch: function () {
        // // 展示本地存储能力
        // var logs = wx.getStorageSync('logs') || []
        // logs.unshift(Date.now())
        // wx.setStorageSync('logs', logs)
        // // 登录
        // wx.login({
        //     success: res => {
        //         // 发送 res.code 到后台换取 openId, sessionKey, unionId
        //     }
        // })
        // // 获取用户信息
        // wx.getSetting({
        //     success: res => {
        //         if (res.authSetting['scope.userInfo']) {
        //             // 已经授权，可以直接调用 getUserInfo 获取头像昵称，不会弹框
        //             wx.getUserInfo({
        //                 success: res => {
        //                     // 可以将 res 发送给后台解码出 unionId
        //                     this.globalData.userInfo = res.userInfo

        //                     // 由于 getUserInfo 是网络请求，可能会在 Page.onLoad 之后才返回
        //                     // 所以此处加入 callback 以防止这种情况
        //                     if (this.userInfoReadyCallback) {
        //                         this.userInfoReadyCallback(res)
        //                     }
        //                 }
        //             })
        //         }
        //     }
        // })
    },
    getRealJsonData(jsonData) {
        // 清洗商品(ProData)数据
        if (jsonData.ProData != null) {
            var i, n = jsonData.ProData.length;
            for (i = 0; i < n; i++) {
                jsonData.ProData[i].price = jsonData.ProData[i].price.toFixed(2);
                jsonData.ProData[i].originPrice = jsonData.ProData[i].originPrice.toFixed(2);
            }
        }
        // 清洗购物车(Shops)数据
        else if (jsonData.Shops != null) {
            //console.log(jsonData)
            var i, l = jsonData.Shops.length;
            for (i = 0; i < l; i++) {
                var data = jsonData.Shops[i];
                var j, n = data.CartData.length;
                for (j = 0; j < n; j++) {
                    data.CartData[j].price = data.CartData[j].price.toFixed(2);
                    data.CartData[j].originPrice = data.CartData[j].originPrice.toFixed(2);
                    data.CartData[j].checked = false;
                }
                data.checked = false;
                data.all = "0.00";
                data.discount = "0.00";
            }
        }
        // 清洗支付(PayData)数据
        else if (jsonData.PayData != null) {
            var i, l = jsonData.PayData.length;
            for (i = 0; i < l; i++) {
                var data = jsonData.PayData[i];
                data.price = data.price.toFixed(2);
                data.originPrice = data.originPrice.toFixed(2);
            }
        }
        // 清洗订单(OrderData)数据
        else if (jsonData.OrderData != null) {
            var i, l = jsonData.OrderData.length;
            for (i = 0; i < l; i++) {
                var data = jsonData.OrderData[i];
                // data.originPrice = data.originPrice.toFixed(2);
                // data.discount = data.discount.toFixed(2);
                // data.deliveryFee = data.deliveryFee.toFixed(2);
                data.sum = (data.originPrice - data.discount + data.deliveryFee).toFixed(2);
            }
        }
        // 清洗订单列表(OrderList)数据
        else if (jsonData.OrderList != null) {
            var i, l = jsonData.OrderList.length;
            for (i = 0; i < l; i++) {
                var data = jsonData.OrderList[i];
                var j, n = data.CartData.length;
                for (j = 0; j < n; j++) {
                    data.CartData[j].price = data.CartData[j].price.toFixed(2);
                    data.CartData[j].originPrice = data.CartData[j].originPrice.toFixed(2);
                }
                data.sum = (data.originPrice - data.discount + data.deliveryFee).toFixed(2);
                data.originPrice = data.originPrice.toFixed(2);
                data.discount = data.discount.toFixed(2);
                data.deliveryFee = data.deliveryFee.toFixed(2);
            }
        }
        return jsonData;
    }
})