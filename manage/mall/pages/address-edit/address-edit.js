// pages/address-edit/address-edit.js
var that;
Page({

    /**
     * 页面的初始数据
     */
    data: {

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
        wx.request({
            url: 'url',
            method: 'POST',
            data: that.data.formData,
            dataType: 'json',
            success(res) {
                wx.showToast({
                    title: '保存成功',
                    icon: 'none'
                })
            },
            fail() {
                wx.showToast({
                    title: '连接服务器失败',
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