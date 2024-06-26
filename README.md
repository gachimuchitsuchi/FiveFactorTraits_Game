# FiveFactorTraits_Game
研究用の英単語学習ゲーム

作成環境 　Unity2D　Editor Version 2022.3.7f1  
作成期間　2023年8月 ~ 現在  
作成人数 　1人 （先輩の先行研究の成果物に機能を追加） 
# 初めに
このゲームは先行研究にて開発された英単語学習ゲームを参考に開発しています。

私が作成したコードと先行研究にて使用されたコードを別のファイルに分けています。MyScriptsフォルダに私が作成したスクリプトが入っています。ご理解ください。
ReadMeにも※がついたコメントで先行研究で開発されたものと説明している部分があります。
# 研究概要
特性論に基づくゲームプレイヤーのパーソナリティ理解の方法(特性診断)を用いて，診断された特性が好むゲーム要素を英単語学習に複数適用した際の学習効果とモチベーションへの影響を明らかにするための英単語学習アプリケーションを開発する。
## 特性診断
特性診断とは，特性論に基づいて提案された 5つのプレイヤー特性に分類分けするための診断手法である．
### 5 つのプレイヤー特性
- 美的志向（Aesthetic orientation）
この特性の高いプレイヤーは景色を楽しむことや綺麗なグラフィック，アートなどを好む．
- 物語志向（Narrative orientation）
この特性の高いプレイヤーは，複雑な物語やゲーム内のストーリーを好む．
- 目標志向（Goal orientation）
この特性が高いプレイヤーは，ゲームを 100％クリアしたり，全てのコレクションを完成したりすることを好む．
- 挑戦志向（Challenge orientation）
この特性が高いプレイヤーは，難しいゲームや困難な挑戦を好む．
- 社会的志向（Social orientation）
この特性が高いプレイヤーは，一般に他の人と一緒にプレイすることを好む

これらの5つのプレイヤー特性が好むゲーム要素を調査し、好みのゲーム要素として英単語学習アプリケーションに実装する。
# 実装する好みのゲーム要素
室蘭工業大学生６８名に特性診断と好みのゲーム要素のアンケートを取り、実装するゲーム要素を選択した。
- 美的志向(Aesthetic orientation)　クエスト：ゲームプレイヤーに対する所定のミッションやチャレンジ
- 物語志向(Narrative orientation)　アチーブメント：定められた目標
- 目標志向(Goal orientation)　     レベル：プレーヤーの進捗における一定のステップ
- 挑戦志向(Challenge orientation)　ボス戦：あるレベルの頂点における難しいチャレンジ
- 社会的志向(Social orientation)   チーム：共通の目的のために協力するプレーヤーのグループ　**未実装**です。

## クエスト
美的志向のプレイヤーが使用できるゲーム要素。
英単語をタイピングして時間を競うタイピングクエストを作成した。

本筋である英単語の暗記から離れたゲーム体験を行うことで楽しく英語を学ぶことを想定。


https://github.com/gachimuchitsuchi/FiveFactorTraits_Game/assets/101007932/a626ea60-11e1-452c-bd80-4dd65a65f0c3


## アチーブメント
※先行研究にて、実装されています。
物語志向のプレイヤーが使用できるゲーム要素。
用意された実績を模擬英単語試験を通して、解除していく。
実績解除をすることでやる気を上げることができる。


https://github.com/gachimuchitsuchi/FiveFactorTraits_Game/assets/101007932/913e86d2-f21c-4bf8-ab8f-4de39c0a347b


## レベル
目標志向のプレイヤーが使用できるゲーム要素。
レベルが低い英単語から暗記をはじめ、正解していくと経験値を確保できる。
経験値を集め、自身のレベルを上げると次の難しい英単語に挑戦できるようになる。最後のレベルの英単語を暗記するまで、それを繰り返す。
自身の経験がたまっていくことを実感できるので、やる気と達成感を得られる。


https://github.com/gachimuchitsuchi/FiveFactorTraits_Game/assets/101007932/c56717dc-8a14-4cff-a819-26b549ce904b


## ボス戦
挑戦志向のプレイヤーが使用できるゲーム要素。
漢字でGoを参考に作成した。
日本語訳に対応した英単語を答える際に日本語訳がどんどん大きくなり時間を超えるとライフが１減る。
ライフが０になるとゲームオーバーとなる。
現在はゲーム性の実装だけだが、今後漢字でGoのようにボスのようなキャラクターが攻撃を仕掛けてくる演出や退治する演出を作成する予定
中ボス、ボスを倒しながら英単語を暗記してもらうことで快感を得ながら楽しく学ぶことができる。



https://github.com/gachimuchitsuchi/FiveFactorTraits_Game/assets/101007932/384da0dc-b7a4-4402-9306-10c416bc128e



## その他ゲーム要素以外に作成した機能
### カウントダウンクラス
ゲームを始める、制限時間を作るときのカウントダウンを簡単に実装できるように作成した。
delegateを使い、カウントがゼロになったときのイベントを簡単に実装できるようにした。

https://github.com/gachimuchitsuchi/FiveFactorTraits_Game/blob/main/Assets/Scripts/CountDown.cs

# 参考文献
SpriteのPhysicsShapeを使ってuGUIで自由な形のあたり判定を作る（ボタンのあたり判定）
https://qiita.com/sune2/items/cf9ef9d197b47b2d7a10

【Unity】TextMesh ProでOutlineなどの設定を複数保持する方法
https://ekulabo.com/text-mesh-pro-material1

【UnityでRPG製作】レベルアップの実装方法
https://uuc1h.hatenablog.jp/entry/2021/05/18/233137

【Unity】RPGのレベルシステムの書き方
https://qiita.com/azumagoro/items/38a735019d5ce5da7526

[Unity]Awake() Start() 実行順序
https://qiita.com/udonLove/items/fd6c64fcd738dd52f8b6

[Unity]コルーチン中断のやり方
https://www.hanachiru-blog.com/entry/2019/07/16/213033

【Unity】アニメーションするダイアログの作り方
https://nekojara.city/unity-animated-dialog

RPGどっと素材
http://rpgdot3319.g1.xrea.com/muz/002.html

[Unity] UIを拡大　縮小させる方法
https://c-taquna.hatenablog.com/entry/2018/12/02/003621

[Unity] Unity(C#)でのデリゲート/イベントのつかいかた
https://aizu-vr.hatenablog.com/entry/2021/08/23/_Unity%28C%23%29%E3%81%A7%E3%81%AE%E3%83%87%E3%83%AA%E3%82%B2%E3%83%BC%E3%83%88/%E3%82%A4%E3%83%99%E3%83%B3%E3%83%88%E3%81%AE%E3%81%A4%E3%81%8B%E3%81%84%E3%81%8B%E3%81%9F

【Unity】カラーコードとColor型を変換する【C#】
https://nekosuko.jp/1674/
