// pages/cart/cart.js
const app = getApp()
var uid = "ding"
import {
    Pro
} from '../../common/model/pro.js'
let pro = new Pro()
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
        currentPage: 'cart'
    },
    shopCheckboxTap: function (e) {
        var sid = e.target.dataset.shopid;
        var i, l = this.data.shops.length;
        //console.log(sid)
        for (i = 0; i < l; i++) {
            if (this.data.shops[i].shopID == sid) {
                var check = !this.data.shops[i].checked
                this.data.shops[i].checked = check

                var cart = this.data.shops[i].CartData;
                var j, n = cart.length;
                for (j = 0; j < n; j++) {
                    this.data.shops[i].CartData[j].checked = check;
                    //console.log(this.data.shops[i].CartData[j])
                    this.setData({
                        shops: this.data.shops
                    })
                    this.getSummary();
                }
                break;
            }
        }
    },
    itemCheckboxTap: function (e) {
        //console.log(e.target.dataset.shopid)
        var sid = e.target.dataset.shopid;
        var cid = e.target.dataset.cid;
        var i, l = this.data.shops.length;
        //console.log(this.data.shops)
        for (i = 0; i < l; i++) {
            if (this.data.shops[i].shopID == sid) {
                var cart = this.data.shops[i].CartData;
                var j, n = cart.length;
                var cnt = 0
                for (j = 0; j < n; j++) {
                    if (cart[j].id == cid) {
                        var check = !cart[j].checked
                        this.data.shops[i].CartData[j].checked = check;
                        //console.log(this.data.shops[i].CartData[j])
                        this.setData({
                            shops: this.data.shops
                        })
                        this.getSummary();
                    }
                    // 需要遍历所有item决定是否修改shop的checkbox，因此此处不要break循环
                    if (cart[j].checked == true) {
                        cnt += 1;
                    }
                }
                // 修改shop的checkbox
                //console.log(cnt)
                if (cnt == n && this.data.shops[i].checked == false) {
                    this.data.shops[i].checked = true;
                    this.setData({
                        shops: this.data.shops
                    })
                } else if (cnt != n && this.data.shops[i].checked == true) {
                    this.data.shops[i].checked = false;
                    this.setData({
                        shops: this.data.shops
                    })
                }
                break;
            }
        }
        this.getSummary();
    },
    itemCheckboxTap2: function (e) {
        let index = e.target.dataset.index;
        let add = this.data.addr;
        let ch = add[index].checked;
        for (let i = 0; i < add.length; i++) {
            if (i != index) {
                add[i].checked = false;
                this.setData({
                    addr: add
                })
            }
        }
        if (ch) {
            add[index].checked = false;
            this.setData({
                addr: add,
                addrsid:add[index].id
            })
        } else {
            add[index].checked = true;
            this.setData({
                addr: add,
                addrsid:add[index].id
            })
        }
    },
    plus: function (e) {
        if (e.target.dataset.count < 99) {
            pro.plus({
                data: {
                    uid: uid,
                    pid: e.target.dataset.pid
                },
                success: (e) => {
                    //console.log(e)
                    this.getCartData()
                }
            })
        } else {
            wx.showModal({
                title: '提示',
                content: '该商品限购99个',
                showCancel: false
            })
        }
    },
    minus: function (e) {
        if (e.target.dataset.count == 1) {
            wx.showModal({
                title: '删除确认',
                content: '该商品在购物车中只剩下1件，是否删除',
                success: (res) => {
                    if (res.confirm == true) {
                        pro.minus({
                            data: {
                                uid: uid,
                                pid: e.target.dataset.pid
                            },
                            success: (e) => {
                                //console.log(e)
                                app.getCartCount((e) => {
                                    app.globalData.toolbar[2].tag = e.data;
                                    this.setData({
                                        tabs: app.globalData.toolbar
                                    })
                                })
                                this.getCartData()
                                //console.log(this.data.tabs)
                            }
                        })
                    }
                }
            })
        }
        else {
            pro.minus({
                data: {
                    uid: uid,
                    pid: e.target.dataset.pid
                },
                success: (e) => {
                    this.getCartData()
                }
            })
        }
    },
    buy: function (e) {
        var list = ""
        var sid = e.target.dataset.shopid;
        var i, l = this.data.shops.length;
        var addrsid=this.data.addrsid;
        //console.log(this.data.shops)
        for (i = 0; i < l; i++) {
            if (this.data.shops[i].shopID == sid) {
                var cart = this.data.shops[i].CartData;
                var j, n = cart.length;
                for (j = 0; j < n; j++) {
                    if (cart[j].checked == true) {
                        if (list == "") {
                            list = cart[j].id
                        } else {
                            list += " " + cart[j].id
                        }
                    }
                }
                break;
            }
        }
        if (list != "") {
            var url = '../pay/pay?sid=' + sid + '&cartid=' + list+ '&addrid=' + addrsid
            wx.redirectTo({
                url: url
            })
        }
    },
    getCartData: function (e) {
        pro.getCartData({
            data: {
                uid: uid
            },
            success: (e) => {
                e.data = app.getRealJsonData(e.data)
                //console.log(e.data)
                this.setData({
                    shops: e.data.Shops
                })
            }
        })
    },

    getSummary: function () {
        var i, l = this.data.shops.length;
        //console.log(this.data.shops)
        for (i = 0; i < l; i++) {
            var sum = 0.0;
            var originSum = 0.0;
            var cart = this.data.shops[i].CartData;
            var j, n = cart.length;
            for (j = 0; j < n; j++) {
                if (cart[j].checked == true) {
                    var count = Number(cart[j].count);
                    sum += Number(cart[j].price) * count;
                    originSum += Number(cart[j].originPrice) * count;
                }
            }
            this.data.shops[i].all = sum.toFixed(2);
            this.data.shops[i].discount = (originSum - sum).toFixed(2);
            this.setData({
                shops: this.data.shops
            })
            //console.log(this.data.shops[i])
        }
    },
    getAddress: function (e) {
        user.getAddress({
            data: {
                uid: 'ding'
            },
            success: (e) => {
                let dd = e.data.AddressData;
                for (let index = 0; index < dd.length; index++) {
                    const element = dd[index];
                    element.checked = false;
                }
                this.setData({
                    addr: dd
                })
                //console.log(this.data.addr)
            }
        })
    },
    /**
     * 生命周期函数--监听页面加载
     */
    onLoad: function (options) {
        this.setData({
            tabs: app.globalData.toolbar
        })
        this.getCartData()
        this.getAddress()
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