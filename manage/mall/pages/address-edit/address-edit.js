// pages/address-edit/address-edit.js
var that;
var util = require('../../common/utils/util')
Page({

    /**
     * 页面的初始数据
     */
    data: {
        sjr: '',
        dh: '',
        sf: '',
        cs: '',
        xq: '',
        jtdz: '',
    },
    sjr: function (e) {
        this.setData({
            sjr: e.detail.value
        })
    },
    dh: function (e) {
        this.setData({
            dh: e.detail.value
        })
    },
    sf: function (e) {
        this.setData({
            sf: e.detail.value
        })
    },
    cs: function (e) {
        this.setData({
            cs: e.detail.value
        })
    },
    xq: function (e) {
        this.setData({
            xq: e.detail.value
        })
    },
    jtdz: function (e) {
        this.setData({
            jtdz: e.detail.value
        })
    },
    /**
     * 生命周期函数--监听页面加载
     */
    onLoad: function (options) {
        that = this
    },

    /**
     * 生命周期函数--监听页面初次渲染完成
     */
    onReady: function () {

    },
    formInput: function (e) {
        let name = e.currentTarget.dataset.name;
        this.setData({
            [`formData.${name}`]: e.detail.value
        })
    },
    save: function () {
        var that = this;
        /* var sfzh = wx.getStorageSync("id");*/
        let data = {
            dm: 'admin',
            name: that.data.sjr,
            phone: that.data.dh,
            province: that.data.sf,
            city: that.data.cs,
            local: that.data.xq,
            address1: that.data.jtdz,
            uid: 1
        };
        console.log(util.formatterData(data));
        wx.request({
            url: 'http://localhost:58843/Controllers/AdressHandler.ashx?method=Add',
            method: 'get',
            dataType: 'json',
            data: {
                data: util.formatterData(data)
            },
            success: function (res) {
                if (res.data.return_code == 'Success') {
                    wx.showToast({
                        title: '登记成功',
                        icon: 'none'
                    });
                } else {
                    wx.showToast({
                        title: '登记失败',
                        icon: 'none'
                    })
                }
            },
            fail: function () {
                wx.showToast({
                    title: '连接服务器时失败',
                    icon: 'none'
                })
            }
        })
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