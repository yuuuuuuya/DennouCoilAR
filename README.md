# DennouCoil

ARFoundationを使い、電脳コイル(アニメ)の世界をイメージしたARアプリ。<br>
メタバグ(宝石のようなもの)を集めたり、古い空間に入ったりする事が可能<br>

# DEMO

[![demo](https://github.com/yuuuuuuya/DennouCoil/wiki/images/DennouCoil.gif)](https://github.com/yuuuuuuya/DennouCoil/wiki/images/DennouCoil.gif)

# Implementation

- 一部分のみポストエフェクトをかけない<br>
Quadオブジェクトと3大のカメラを用意。
カメラの種類は、<br>
(1)Quadオブジェクト以外をレンダリングし、ポストエフェクトをかけたMainカメラ。<br>
(2)Quadオブジェクトのみレンダリングし、ポストエフェクトをかけないカメラ。<br>
(3)QuadオブジェクトのTextureにレンダリングするカメラ。<br>
以上でQuadオブジェクトの部分のみエフェクトをかけないように出来る。

- 扉の正面から入ると、表示されるオブジェクトを切り替える。<br>
扉からカメラへの方向ベクトルを取得し、z軸が0以上なら正面と判定。<br>
扉とカメラの衝突時に正面の場合、カメラのレイヤーマスクを変更し切り替える。

- 扉とキラバグ(宝石)をタップ<br>
画面をタップ時にraycastを飛ばす。<br>
ヒットしたオブジェクトのタグで場合分けをしオブジェクト毎に処理を行う。

# Usage

- 黒い壁を数回タッチ→古い空間へのゲートが登場<br>
- メタバグをシングルタッチ→取得<br>
- 画面を長押し→ウィンドウの表示・非表示切替。ウィンドウには集めたメタバグの量が表示

# Author

- 伊島悠矢
- b.ald.m.wn@gmail.com