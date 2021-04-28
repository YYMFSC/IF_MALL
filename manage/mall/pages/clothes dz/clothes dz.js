// pages/Clothing Reservation/Clothing Reservation.js
var ajax = require('../../common/ajax/ajax.js'),
  config = require('../../common/data/config.js');
Page({



  /**
   * 页面的初始数据
   */
  data: {
    iscomeRegister:"1",
    isgoRegister: "1",
    isSetting1:true,//来程是否登记，不登记为true
    isSetting2:false,//是否要进行来程设定，点击为true
    isSetting3:true,//去程是否登记，不登记为true
    state:1,//点击来程为1，点击去程为2
    picker1Value: 0,
    picker1Name:0,
    picker1Range: ['请选择', '1件', '2件', '3件', '3件以上'],
    picker2Value: 0,
    picker2Name: 0,
    picker2Range: ['请选择', '21岁-30岁', '31岁-40岁', '41岁-50岁'],
    picker3Value: 0,
    picker3Name: 0,
    picker3Range: ['请选择', '水晶城', '运河上街', '乐堤港'],
    dateValue: '请选择日期',
    timeValue: '请选择时间',
    comeNumber:0,
    comeMethod:0,
    comePlace:0,
    comeDate:0,
    returnNumber:0,
    returnPlace:0,
    returnDate:0,


    select:false,
    grade_name:'--请选择--',
    grades: [
      '真丝',
      '织锦缎',
      '棉提花',
      '棉印花',
      '醋酸面料',
      '欧根纱',
      '镂空绣花',
      '毛呢面料',
     ]
   },/**
   * 点击下拉框
   */
   bindShowMsg() {
    this.setData({
     select: !this.data.select
    })
   },
  /**
   * 已选下拉框
   */
   mySelect(e) {
    console.log(e)
    var name = e.currentTarget.dataset.name
    this.setData({
     grade_name: name,
     select: false
    })
  },
  add(e) {
    this.setData({ JK: parseInt(this.data.JK + 1) })
  },
  subtract(e) {
    if (this.data.JK> 1)
      this.setData({ JK: parseInt(this.data.JK - 1) })
  },
  add1(e) {
    this.setData({ XW: parseInt(this.data.XW + 2) })
  },
  subtract1(e) {
    if (this.data.XW > 1)
    this.setData({ XW: parseInt(this.data.XW - 1) })
  },
  add2(e) {
     this.setData({ YW: parseInt(this.data.YW + 1) })
  },
  subtract2(e) {
    if (this.data.YW > 1)
      this.setData({ YW: parseInt(this.data.YW - 1) })
  },
  add3(e) {
    
    this.setData({ TW: parseInt(this.data.TW + 1) })
  },
  subtract3(e) {
    if (this.data.TW> 1)
      this.setData({ TW: parseInt(this.data.TW - 1) })
  },
  add4(e) {
    
    this.setData({ XC: parseInt(this.data.XC + 1) })
  },
  subtract4(e) {
    if (this.data.XC> 1)
      this.setData({ XC: parseInt(this.data.XC - 1) })
  },
  add5(e) {
    
    this.setData({ QC: parseInt(this.data.QC + 1) })
  },
  subtract5(e) {
    if (this.data.QC> 1)
      this.setData({ QC: parseInt(this.data.QC - 1) })
  },

  getJK: function (e) {
    this.setData({
      JK: e.detail.value
    })
    console.log(this.data.JK)
  },
  getXW: function (e) {
    this.setData({
      XW: e.detail.value
    })
    console.log(this.data.XW)
  },
  getYW: function (e) {
    this.setData({
      YW: e.detail.value
    })
    console.log(this.data.YW)
  },
  getTW: function (e) {
    this.setData({
      TW: e.detail.value
    })
    console.log(this.data.TW)
  },
  getXC: function (e) {
    this.setData({
      XC: e.detail.value
    })
    console.log(this.data.XC)
  },
  getQC: function (e) {
    this.setData({
      QC: e.detail.value
    })
    console.log(this.data.QC)
  },
 
 
  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    
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
    var sfzh = wx.getStorageSync("id")
    ajax.query({
      url: config.IF_Url,
      param: { method: "GetjxMessage", sfzh:sfzh },
      callback: function (res) {
        console.log(res)
        if (res.Table[0].sg != null) {
          that.setData({ height: res.Table[0].sg, weight: res.Table[0].jm, isRegister: 2 ,isBooking:true})
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