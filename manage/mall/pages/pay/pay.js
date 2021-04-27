// pages/pay/pay.js
const app = getApp()
import {
    Order
} from '../../common/model/order.js'
let order = new Order()
import {
    User
} from '../../common/model/user.js'
let user = new User()
var remark = ""
Page({

    /**
     * 页面的初始数据
     */
    data: {
        payway: 1,
        showCount: true
    },
    inputKey: function (e) {
        remark = e.detail.value;
    },
    payway: function (e) {
        this.setData({
            payway: e.target.dataset.payway
        })
        //console.log(this.data.payway)
    },
    submitOrder: function (e) {
        //console.log(this.data)
        order.submitOrder({
            data: {
                uid: 'ding',
                sid: this.data.shopid,
                cartIdList: this.data.cartlist,
                payway: this.data.payway,
                originPrice: this.data.originall,
                discount: this.data.discount,
                deliveryFee: this.data.deliverFee,
                remark: remark
            },
            success: (e) => {
                //console.log(e)
                if (e.statusCode == 200) {
                    if (this.data.payway == 1) {
                        wx.redirectTo({
                            url: '../paying/paying?orderid=' + e.data
                        })
                    } else {
                        wx.redirectTo({
                            url: '../pay-success/pay-success'
                        })
                    }
                }
            }
        })
    },
    getPayData: function (e) {
        order.getPayData({
            data: {
                uid: 'ding',
                idlist: this.data.cartlist
            },
            success: (e) => {
                //console.log(e.data)
                e.data = app.getRealJsonData(e.data)
                //console.log(e.data)
                this.setData({
                    payData: e.data.PayData,
                    proData: e.data.PayData
                })
                this.getSummary()
            }
        })
    },
    getSummary: function () {
        var sum = 0.0;
        var originSum = 0.0;
        var i, l = this.data.payData.length;
        for (i = 0; i < l; i++) {
            var count = Number(this.data.payData[i].count);
            sum += Number(this.data.payData[i].price) * count;
            originSum += Number(this.data.payData[i].originPrice) * count;
        }
        this.setData({
            originall: originSum.toFixed(2)
        })
        this.setData({
            discount: (originSum - sum).toFixed(2)
        })
        var deliver = 0.00
        if (sum < 55) {
            deliver = 5.00
        }
        this.setData({
            deliverFee: deliver.toFixed(2)
        })
        this.setData({
            all: (sum + deliver).toFixed(2)
        })
        //console.log(this.data)
    },
    getAddress: function (e) {
        user.getAddress({
            data: {
                uid: 'ding'
            },
            success: (e) => {
                this.setData({
                    addr: e.data.AddressData[0]
                })
                //console.log(this.data.addr)
            }
        })
    },
    /**
     * 生命周期函数--监听页面加载
     */
    onLoad: function (options) {
        //console.log(options)
        this.setData({
            cartlist: options.cartid,
            shopid: options.sid
        })
        this.getPayData()
        this.getAddress()
    },

    /**
     * 生命周期函数--监听页面初次渲染完成
     */
    onReady: function () {

    },

    /**
     * 生命周期函数--监听页面显示
     */
    onShow: function () {

    },

    /**
     * 生命周期函数--监听页面隐藏
     */
    onHide: function () {

    },

    /**
     * 生命周期函数--监听页面卸载
     */
    onUnload: function () {

    },

    /**
     * 页面相关事件处理函数--监听用户下拉动作
     */
    onPullDownRefresh: function () {

    },

    /**
     * 页面上拉触底事件的处理函数
     */
    onReachBottom: function () {

    },

    /**
     * 用户点击右上角分享
     */
    onShareAppMessage: function () {

    }
})