// pages/Notice/Notice.js
var ajax = require('../../common/ajax/ajax.js'),
  config = require('../../common/data/config.js');
var sfzh = wx.getStorageSync("id")
Page({

  /**
   * 页面的初始数据
   */
  data: {
    noticeList: [
      { title: "消息报到须知",isread:"未读",day:"2018-05-02"},
      { title: "床上用品说明", isread: "未读", day: "2018-05-02"},
      { title: "新生报到接送和交通", isread: "未读", day: "2018-05-02"},
      { title: "志愿者须知", isread: "未读",day: "2018-05-02"},
      { title: "床上用品预定", isread: "未读",day: "2018-05-02"}
    ],
    noticeReadList:""
  },
  GetggReadMessage(e){
    var that=this;
    ajax.query({
      url: config.IF_Url,
      param: { method: "GetggReadMessage",sfzh:sfzh },
      callback: function (res) {
        console.log(res)
        that.setData({noticeReadList:res.Table})
        console.log(that.data.noticeReadList)
          
      }
    })
  },
  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    
  },
  go(e){
    var id = e.currentTarget.dataset.id;
    var noticeList=this.data.noticeList;
    var noticeList1="";
    for (var i = 0; i < noticeList.length;i++)
    {
       if(noticeList[i].id==id)
       noticeList1=noticeList[i];
    }
     wx.navigateTo({
       url: '../NoticeRead/NoticeRead?title=' + noticeList1.title + '&time=' + noticeList1.AddTime + '&id=' + id + '&content=' + noticeList1.content
     })
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
    var that = this;
    that.GetggReadMessage()
    //数据读取
    ajax.query({
      url: config.IF_Url,
      param: { method: "GetggMessage" },
      callback: function (res) {
        console.log(res)
        that.setData({ noticeList: res.Table })
        console.log(that.data.noticeList, res.Table.length)
        for (var i = 0; i < res.Table.length; i++) {
          var up = 'noticeList[' + i + '].isRead'
          for (var j = 0; j < that.data.noticeReadList.length; j++) {
            if (res.Table[i].id == that.data.noticeReadList[j].noticeId) {
              that.setData({ [up]: "已读" })
              break;
            }
            else { that.setData({ [up]: "未读" }) }
          }
          if (that.data.noticeReadList.length == 0)
            that.setData({ [up]: "未读" })
        }

      }
    })
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