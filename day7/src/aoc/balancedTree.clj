(ns aoc.balancedTree
    (:require [clojure.string :as str] )
)

(defn make-node [line]
    (def node (str/split line #" "))
    (def newNode [
        (get node 0)
        (Integer. (str/replace (get node 1) #"\(|\)" ""))
        (if (re-find #"->" line) 
            (map str/trim (str/split (get (str/split line #"->") 1) #"(,\s)+"))
            '())
        ])
    newNode)

(defn make-tree [data]
    (def lines (str/split data #"\n"))
    (map make-node lines)
)

(defn find-child [tree childName]
    (filter (fn [x] (not (empty? (filter #(= % childName) (get x 2))))) tree)
)

(defn find-bottom [tree]
    (first (filter (fn [x] (empty? (find-child tree (get x 0) ))) tree))
)

(declare get-total-weight)

(defn get-child-weight [tree node]
    (def children (get node 2))
    (def weights (map #(get-total-weight tree %) children))
    (reduce + weights)
)

(defn get-total-weight [tree nodeName]
    (def node (first (filter #(= (get % 0) nodeName) tree)))
    (if (empty? (get node 2)) 
        (get node 1) 
        (+ (get node 1) (get-child-weight tree node))
    )
)

;(defn find-unbalance [tree node]
;)