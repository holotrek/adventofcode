(ns aoc.balancedTree
    (:require [clojure.string :as str] )
)

(defn make-node [line]
    (let [
        node (str/split line #" ")
        newNode [
            (get node 0)
            (Integer. (str/replace (get node 1) #"\(|\)" ""))
            (if (re-find #"->" line) 
                (map str/trim (str/split (get (str/split line #"->") 1) #"(,\s)+"))
                '())
        ]]
        newNode
    )
)

(defn make-tree [data]
    (let [lines (str/split data #"\n")]
        (map make-node lines)
    )
)

(defn find-parent-of [tree childName]
    (first (filter (fn [x] (not (empty? (filter #(= % childName) (get x 2))))) tree))
)

(defn find-bottom [tree]
    (first (filter (fn [x] (not (find-parent-of tree (get x 0) ))) tree))
)

(declare get-total-weight)

(defn get-child-weight [tree node]
    (let [children (get node 2)
        weights (map #(get-total-weight tree %) children)]
        (reduce + weights)
    )
)

(defn get-node [tree nodeName]
    (first (filter #(= (get % 0) nodeName) tree))
)

(defn get-total-weight [tree nodeName]
    (let [node (get-node tree nodeName)]
        (if (empty? (get node 2)) 
            (get node 1) 
            (+ (get node 1) (get-child-weight tree node))
        )
    )
)

(defn find-different-weight [weights]
    (let [l (seq weights)
        chk (get (first l) 1)
        ne (filter #(not= (get % 1) chk) l)]
        (if (= (count ne) 1)
            (get (first ne) 0)
            (get (first l) 0)
        )
    )
)

(defn get-sibling-weight [tree node]
    (let [parent (find-parent-of tree (get node 0))
        children (get parent 2)
        otherChild (first (filter #(not= % (get node 0)) children))]
        (get-total-weight tree otherChild)
    )
)

(defn find-unbalance [tree & nodeName]
    (let [node 
        (if (empty? nodeName)
            (find-bottom tree)
            (get-node tree nodeName)
        )
        children (get node 2)
        weightMap (into {} (map (fn [x] [x (get-total-weight tree x)]) children))
        weights (map (fn [x] (get x 1)) weightMap)]
        (if (every? #{(first weights)} weights)
            [nodeName (- (get-sibling-weight tree node) (get-child-weight tree node))]
            (recur tree (find-different-weight weightMap))
        )
    )
)
