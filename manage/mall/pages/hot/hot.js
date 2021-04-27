// pages/hot/hot.js
const app = getApp()
import {
    Pro
} from '../../common/model/pro.js'
let pro = new Pro()
Page({

    /**
     * 页面的初始数据
     */
    data: {

    },
    toProduct: function(e) {
        //console.log(e.currentTarget.dataset.proId)
        var url = '../product/product?pid=' + e.currentTarget.dataset.proId
        wx.navigateTo({
            url: url,
        })
    },
    cartAdd: function(e) {
        //console.log(e.target.dataset.price)
        pro.plus({
            data: {
                uid: 'ding',
                pid: e.target.dataset.proId,
                price: e.target.dataset.price
            },
            success: (e) => {
                //console.log(e)
                app.getCartCount((e) => {
                    //console.log(e.data)
                    app.globalData.toolbar[2].tag = e.data;
                    this.setData({ tabs: app.globalData.toolbar })
                    //console.log(this.globalData.toolbar)
                })
            }
        })        
    },
    getProData: function () {
        pro.getProData({
            success: (e) => {
            e.data = app.getRealJsonData(e.data)
            //console.log(e.data);
            this.setData({
                proData: e.data.ProData
            });
        }})
    },
    /**
     * 生命周期函数--监听页面加载
     */
    onLoad: function (options) {
        this.getProData()
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