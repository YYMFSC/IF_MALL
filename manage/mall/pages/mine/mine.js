// pages/mine/mine.js
var app = getApp();
import {
    User
} from '../../common/model/user.js'
let user = new User()
Page({

    /**
     * 页面的初始数据
     */
    data: {
        tabs: null,
        user: {
            userInfo: {
                name: '林丹',
                degree: 8
            },
            account: [{
                num: '4',
                text: '收藏夹'
            }, {
                num: '296',
                text: '足迹'
            }, {
                num: '3',
                text: '红包卡券'
            }]
        },
        currentPage: 'mine'
    },

    /**
     * 生命周期函数--监听页面加载
     */
    onLoad: function (options) {
        this.setData({
            tabs: app.globalData.toolbar
        })
        this.setData({
            userInfo: app.globalData.userInfo
        })
        // app.getTabs((e) => {
        //     //console.log(e.data)
        //     this.setData({ tabs: e.data.Tabs })
        // })
        if (this.data.userInfo == null) {
            wx.login({
                complete: (res) => {
                    console.log(res)
                    if (!res.code) {
                        return;
                    }
                    // user.login({
                    //     data: {
                    //         code: res.code
                    //     },
                    //     success: (e)=>{
                    //         console.log(e)
                    //     }
                    // })
                },
            })
        }
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