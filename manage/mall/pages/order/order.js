// pages/order/order.js
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
        currentTab: -1,
        showCount: true,
        navbar: [{
            text: '全部订单',
            value: -1
        }, {
            text: '待付款',
            value: 0
        }, {
            text: '待发货',
            value: 1
        }]
    },
    tapTab: function (e) {
        var val = e.target.dataset.val;
        this.setData({
            currentTab: val
        })
        this.getCartListData()
    },
    getCartListData: function(e) {
        order.getCartList({
            data: {
                uid: 'ding',
                ispay: (this.data.currentTab < 0 ? '%':this.data.currentTab)
            },
            success: (e) => {
                //console.log(e.data)
                e.data = app.getRealJsonData(e.data);
                var sda=e.data.OrderList
                for (let index = 0; index < sda.length; index++) {
                    if(sda[index].isPay==0)
                    {
                        sda[index].isPay='待付款';
                    }
                    if(sda[index].isPay==1)
                    {
                        sda[index].isPay='待发货';
                    }
                    if(sda[index].isPay==2)
                    {
                        sda[index].isPay='待收货';
                    }
                    
                }
                this.setData({ cartList: sda})
            }
        })
    },
    /**
     * 生命周期函数--监听页面加载
     */
    onLoad: function (options) {
        this.getCartListData()
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