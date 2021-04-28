// pages/paying/paying.js
var app = getApp()
import {
    Order
} from '../../common/model/order.js'
let order = new Order()
Page({

    /**
     * 页面的初始数据
     */
    data: {
        payway: 1
    },
    payway: function (e) {
        this.setData({
            payway: e.target.dataset.payway
        })
        //console.log(this.data.payway)
    },
    pay: function (e) {
        wx.showToast({
            title: '下单中',
            icon: 'loading',
            duration: 5000
        })
        order.pay({
            data: {
                orderid: this.data.orderid,
                payway: this.data.payway
            },
            success: (e) => {
                if (e.statusCode == 200) {
                    wx.hideToast({
                        complete: (res) => {
                            wx.redirectTo({
                                url: '../pay-success/pay-success',
                            })
                        },
                    })
                } else {
                    console.log(e)
                }
            }
        })
    },
    getOrderData: function (e) {
        order.getOrderData({
            data: {
                orderid: this.data.orderid
            },
            success: (e) => {
                e.data = app.getRealJsonData(e.data)
                //console.log(e.data)
                this.setData({
                    orderData: e.data.OrderData[0]
                })
                //console.log(this.data.orderData)
            }
        })
    },
    /**
     * 生命周期函数--监听页面加载
     */
    onLoad: function (options) {
        //console.log(options)
        this.setData({
            orderid: options.orderid
        })
        this.getOrderData();
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