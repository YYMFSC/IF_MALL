// pages/product/product.js
const app = getApp();
import {
    Pro
} from '../../common/model/pro.js'
let pro = new Pro()
Page({

    /**
     * 页面的初始数据
     */
    data: {
        dotColor: 'rgba(254,100,46,.8)'
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
                    this.updateCartCount()
                    //console.log(this.globalData.toolbar)
                })
            }
        })        
    },
    getProData: function(e) {
        pro.getProData({
            data: {
                pid: this.data.query.pid
            },
            success: (e) => {
                e.data = app.getRealJsonData(e.data)
                if (e.data.ProData[0].from == null) {
                    e.data.ProData[0].from = '其他'
                }
                if (e.data.ProData[0].variety == null) {
                    e.data.ProData[0].variety = '未知'
                }
                this.setData({
                    proData: e.data.ProData[0]
                })
                //console.log(this.data.proData)
                this.imgToSliderData(this.data.proData.img)
            }
        })
    },
    imgToSliderData: function(imgurl) {
        var dict = {
            img: imgurl
        };
        var list = new Array(1);
        list[0] = dict;
        this.setData({ swiperData: list })
        //console.log(this.data.swiperData)
    },
    /**
     * 生命周期函数--监听页面加载
     */
    onLoad: function(options) {
        //console.log(options)
        this.setData({
            query: options
        })
        //console.log(this.data.query.pid)
        this.updateCartCount()
        this.getProData()
    },
    updateCartCount: function() {
        app.getCartCount((e) => {
            //console.log(e.data)
            app.globalData.toolbar[2].tag = e.data;
            this.setData({cartcount: e.data})
        })
    },
    /**
     * 生命周期函数--监听页面初次渲染完成
     */
    onReady: function() {

    },

    /**
     * 生命周期函数--监听页面显示
     */
    onShow: function() {

    },

    /**
     * 生命周期函数--监听页面隐藏
     */
    onHide: function() {

    },

    /**
     * 生命周期函数--监听页面卸载
     */
    onUnload: function() {

    },

    /**
     * 页面相关事件处理函数--监听用户下拉动作
     */
    onPullDownRefresh: function() {

    },

    /**
     * 页面上拉触底事件的处理函数
     */
    onReachBottom: function() {

    },

    /**
     * 用户点击右上角分享
     */
    onShareAppMessage: function() {

    }
})