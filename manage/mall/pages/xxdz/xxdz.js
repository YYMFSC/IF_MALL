// pages/scheduling/scheduling.js
var ajax = require('../../common/ajax/ajax.js'),
  config = require('../../common/data/config.js');
   var util=require('../../common/utils/util')
Page({

  /**
   * 页面的初始数据
   */
  data: {
    iscomeRegister: "1",
    isgoRegister: "1",
    isSetting1: true, //来程是否登记，不登记为true
    isSetting2: false, //是否要进行来程设定，点击为true
    isSetting3: true, //去程是否登记，不登记为true
    state: 1, //点击来程为1，点击去程为2
    picker1Value: 0,
    picker1Name: 0,
    picker1Range: ['请选择', '1件', '2件', '3件', '3件以上'],
    picker2Value: 0,
    picker2Name: 0,
    picker2Range: ['请选择', '21岁-30岁', '31岁-40岁', '41岁-50岁'],
    picker3Value: 0,
    picker3Name: 0,
    picker3Range: ['请选择', '水晶城', '运河上街', '乐堤港'],
    dateValue: '请选择日期',
    timeValue: '请选择时间',
    comeNumber: 0,
    comeMethod: 0,
    comePlace: 0,
    comeDate: 0,
    returnNumber: 0,
    returnPlace: 0,
    returnDate: 0,

  },
  timePickerBindchange: function (e) {
    this.setData({
      timeValue: e.detail.value
    })
  },
  datePickerBindchange: function (e) {
    this.setData({
      dateValue: e.detail.value
    })
  },
  normalPickerBindchange: function (e) {
    this.setData({
      picker1Value: e.detail.value,
      picker1Name: this.data.picker1Range[e.detail.value],
    })
  },
  normalPickerBindchange2: function (e) {
    this.setData({
      picker2Value: e.detail.value,
      picker2Name: this.data.picker2Range[e.detail.value],
    })
  },
  normalPickerBindchange3: function (e) {
    this.setData({
      picker3Value: e.detail.value,
      picker3Name: this.data.picker3Range[e.detail.value],
    })
  },
  //点击来程设定
  comeSetting: function (e) {
    this.setData({
      isSetting2: true,
      state: 1,
      picker1Value: 0,
      picker2Value: 0,
      picker3Value: 0,
      dateValue: '请选择日期',
      timeValue: '请选择时间',
    })
    console.log(this.data.isSetting2, this.data.state)
  },
  //点击去程设定
  returnSetting: function (e) {
    this.setData({
      isSetting2: true,
      state: 2,
      picker1Value: 0,
      picker2Value: 0,
      picker3Value: 0,
      dateValue: '请选择日期',
      timeValue: '请选择时间',
    })
  },
  close: function (e) {
    this.setData({
      isSetting2: false
    })
  },
  register: function (e) {
    /*   var state = this.data.state; */
    var that = this;
    /* var sfzh = wx.getStorageSync("id");*/
    let data = {
      nub: that.data.picker1Name,
      age: that.data.picker2Name,
      adress: that.data.picker3Name,
      time: that.data.dateValue + " " + that.data.timeValue,
      ctime: new Date().toLocaleDateString(),
      uid: 1
    };
  

    wx.request({
      url: 'http://localhost:58843/Controllers/DoorHandler.ashx?method=Add',
      method: 'get',
      dataType: 'json',
      data: {
        data: util.formatterData(data)
      },
      success: function (res) {
        if(res.data.return_code=='Success') {
          that.setData({
            comeNumber: that.data.picker1Name,
            comeMethod: that.data.picker2Name,
            comePlace: that.data.picker3Name,
            comeDate: that.data.dateValue + " " + that.data.timeValue,
            isSetting2: false,
            isSetting1: false
          });
          wx.showToast({
            title: '登记成功',
            icon: 'none'
          })
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

    /* if(state==1)
    {
      if (this.data.iscomeRegister == 1) {
      this.setData({ comeNumber: this.data.picker1Name, comeMethod: this.data.picker2Name, comePlace: this.data.picker3Name, comeDate: this.data.dateValue + " " + this.data.timeValue, isSetting2: false, isSetting1:false})
        console.log(that.data.comeNumber.substring(0,1), that.data.comeMethod, that.data.picker3Value, that.data.comeDate)
      ajax.query({
        url: config.IF_Url,
        param: { method: "AddcomeMessage", sfzh: sfzh, comeNumber: that.data.comeNumber, comeMethod: that.data.comeMethod, comePlace: that.data.picker3Value, comeDate: that.data.comeDate},
        callback: function (res) {
          
          if (res == 'true') {
              wx.showToast({
                title: "登记成功",
              })
          }
          else {
              wx.showToast({
                title: "登记失败",
              })
          }
        }
      })
      }
      else{
        this.setData({ comeNumber: this.data.picker1Range[this.data.picker1Value], comeMethod: this.data.picker2Range[this.data.picker2Value], comePlace: this.data.picker3Name, comeDate: this.data.dateValue + " " + this.data.timeValue, isSetting2: false, isSetting1: false })
        console.log(that.data.comeNumber.substring(0, 1), that.data.comeMethod, that.data.picker3Value, that.data.comeDate)
        ajax.query({
          url: config.IF_Url,
          param: { method: "ModifycomeMessage", sfzh: sfzh, comeNumber: that.data.comeNumber, comeMethod: that.data.comeMethod, comePlace: that.data.picker3Value, comeDate: that.data.comeDate},
          callback: function (res) {
            if (res == 'true') {

              wx.showToast({
                title: "修改成功",
              })
            }
            else {
              wx.showToast({
                title: "修改失败",
              })
            }
          }
        })
      }
    }
    else
    {
      if (this.data.isgoRegister == 1){
      this.setData({ returnNumber: this.data.picker1Name, returnPlace: this.data.picker3Name, returnDate: this.data.dateValue + " " + this.data.timeValue, isSetting2: false, isSetting3: false })
        console.log(that.data.returnNumber, that.data.picker3Value, that.data.comeDate);
      ajax.query({
        url: config.IF_Url,
        param: { method: "AddgoMessage", sfzh: sfzh, comeNumber: that.data.returnNumber, comePlace: that.data.picker3Value, comeDate: that.data.returnDate},
        callback: function (res) {
          if (res == 'true') {
              wx.showToast({
                title: "登记成功",
              })
            }
          else {
              wx.showToast({
                title: "登记失败",
              })
          }
        }
      })
    }
    else{
        this.setData({ returnNumber: this.data.picker1Range[this.data.picker1Value], returnPlace: this.data.picker3Name, returnDate: this.data.dateValue + " " + this.data.timeValue, isSetting2: false, isSetting3: false })
        console.log(that.data.returnNumber, that.data.picker3Value, that.data.comeDate,2345);
        ajax.query({
          url: config.IF_Url,
          param: { method: "ModifygoMessage", sfzh: sfzh, comeNumber: that.data.returnNumber, comePlace: that.data.picker3Value, comeDate: that.data.returnDate},
          callback: function (res) {
            if (res == 'true') {
              wx.showToast({
                title: "修改成功",
              })
            }
            else {
              wx.showToast({
                title: "修改失败",
              })
            }
          }
        })
    }
    } */
    that.onShow();
  },
  modifycome(e) {
    var that = this;
    var picker2Range = that.data.picker2Range;
    var sfzh = wx.getStorageSync("id")
    ajax.query({
      url: config.IF_Url,
      param: {
        method: "GetcomeMessage",
        sfzh: sfzh
      },
      callback: function (res) {
        console.log(res)
        if (res.Table.length != 0) {
          for (var i = 0; i < picker2Range.length; i++) {
            if (picker2Range[i] == res.Table[0].way) {
              var d = i;
            }
            console.log(d, 5677)
          }
          that.setData({
            isSetting2: true,
            state: 1,
            picker1Value: res.Table[0].peopleCount,
            picker2Value: d,
            picker3Value: res.Table[0].pointId,
            dateValue: res.Table[0].arriveTime.substring(0, 10),
            timeValue: res.Table[0].arriveTime.substring(11, 16)
          })
        }
      }
    })
  },
  modifygo(e) {
    var that = this;
    var sfzh = wx.getStorageSync("id")
    ajax.query({
      url: config.IF_Url,
      param: {
        method: "GetgoMessage",
        sfzh: sfzh
      },
      callback: function (res) {
        console.log(res)
        that.setData({
          isSetting2: true,
          state: 2,
          picker1Value: res.Table[0].peopleCount,
          picker3Value: res.Table[0].pointId,
          dateValue: res.Table[0].arriveTime.substring(0, 10),
          timeValue: res.Table[0].arriveTime.substring(11, 16)
        })
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

  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {
    var that = this;
    var sfzh = wx.getStorageSync("id")
    /* ajax.query({
      url: config.IF_Url,
      param: { method: "GetcomeMessage", sfzh:sfzh},
      callback: function (res) {
        console.log(res)
        if(res.Table.length!=0)
        {
          that.setData({ isSetting1: false, comeNumber: res.Table[0].peopleCount+"人", comeMethod: res.Table[0].way, comePlace: res.Table[0].pointName, comeDate: res.Table[0].arriveTime, iscomeRegister: "2" })
        }
      }
    })
    ajax.query({
      url: config.IF_Url,
      param: { method: "GetgoMessage", sfzh: sfzh},
      callback: function (res) {
        console.log(res)
        if (res.Table.length != 0) {
          that.setData({ isSetting3: false, returnNumber: res.Table[0].peopleCount + "人", returnPlace: res.Table[0].pointName, returnDate: res.Table[0].arriveTime, isgoRegister: "2" })
        }
      }
    }) */
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