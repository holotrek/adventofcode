(ns aoc.balancedTree
    (:require [clojure.string :as str] )
)

(defn make-node [line]
    (def node (str/split line #" "))
    (def newNode [
        (get node 0)
        (str/replace (get node 1) #"\(|\)" "")
        (if (re-find #"->" line) 
            (map str/trim (str/split (get (str/split line #"->") 1) #"(,\s)+"))
            '())
        ])
    newNode)

(defn make-tree [data]
    (def lines (str/split data #"\n"))
    (map make-node lines)
)

(defn find-child [tree child]
    (filter 
        (fn [x] 
            (> (count 
                (filter #(= % child) (get x 2))
                ) 0)
        )
    tree)
)

(defn find-bottom [tree]
    (filter 
        (fn [x] 
            (= (count
                (find-child tree (get x 0))
            ) 0)
        )
    tree)
)
