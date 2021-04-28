// pages/login/login.js

const app = getApp();
import {
  User
} from '../../common/model/user.js'
/* let user = new User();
var ajax = require('../../common/ajax/ajax.js'),
  config = require('../../common/data/config.js');
var Mcaptcha = require('../../common/utils/mcaptcha.js'); */
Page({

  /**
   * 页面的初始数据
   */
  data: {
    openid: '',
    name: '',
    imgCode: '',
    typeName: 'name', // 密码框的类型,用于显示密码时更改类型可看见输入的密码而非*号
    passFlag: 1, // 密码第几次点击代表,用于显示不同的图标
    storePass: '', // 暂存密码,用于显示密码
  },
  showPass() { // 显示密码而非*号
    console.log(this.data.storePass, 123)
    if (this.data.passFlag == 1) { // 第一次点击
      this.setData({
        passFlag: 2,
        typeName: 'text'
      });
    } else { // 第二次点击
      this.setData({
        passFlag: 1,
        typeName: 'name'
      });
    }
  },
  // 监听密码input框并把输入的密码在变量storePass下暂存起来,用于在显示密码操作展示
  getopenid: function (e) {
    this.setData({
      openid: e.detail.value,
      storePass: e.detail.value,
    })
    console.log(this.data.openid);
  },
  getname: function (e) {
    this.setData({
      name: e.detail.value
    })
    console.log(this.data.name);
  },

  codeImg: function (e) {
    this.setData({
      imgCode: e.detail.value
    })
    console.log(this.data.imgCode);
  },
  getFormval: function (e) {
    let name = e.currentTarget.dataset.name;
    this.setData({
      [name]: e.detail.value
    });

  },
  //表单提交
  loginClick: function (e) {
    var errMs = '';
    var that = this;
    let uid = that.data.uid;
    let pwd = that.data.pwd;
    if (!uid || !pwd) {
      wx.showToast({
        title: '请输入帐号密码',
        icon: 'none'
      })
      return false;
    }
    console.log("asasdasdasd");
    wx.request({
      url: 'http://localhost:58843/Controllers/LoginHandler.ashx?method=Login', //仅为示例，并非真实的接口地址
      data: {
        uid: uid,
        pwd: pwd,
        kind: '1'
      },
      method:'get',
      dataType:'text',
      success(res) {
        console.log(res.data);
        wx.navigateTo({
          url: '../../pages/home/home',
        })
      },
      fail: function () {
        console.log('x')
        wx.showToast({
          title: '连接服务器失败',
          icon: 'none'
        })
      },complete:function(){
        console.log('c')
      }
    })
    //验证验证码
    /*  var res1 = that.mcaptcha.validate(that.data.imgCode);
     if (that.data.openid == "") {
       errMs = "请输入账号";
     }
     if (that.data.openid.length<1) {
       errMs = "账号有误";
     }
     else if (that.data.name == "") {
       errMs = "请输入密码";
     }
     else if (this.data.codeImg=="") {
       errMs = "请输入验证码";
     }
     else if(that.data.imgCode == '' || that.data.imgCode == null) {
         errMs= "请输入验证码";
     } */






  },

  first(sfzh) {

    //测试是否第一次登录。
    ajax.query({
      url: config.IF_Url,
      param: {
        method: "GetpersonalMessage",
        sfzh: sfzh
      },
      callback: function (res) {
        console.log(res)
        if (res.Table[0] != null) {
          wx.switchTab({
            url: '../home/home'
          })

        } else {
          wx.redirectTo({
            url: '../home/home',
          })
        }
      }
    })
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
   /*  this.mcaptcha = new Mcaptcha({
      el: 'canvas',
      width: 80,
      height: 35,
      createCodeImg: ""
    }); */
  },
  //刷新验证码
  onTap() {
    this.mcaptcha.refresh();
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