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

(defn find-parent-of [tree childName]
    (first (filter (fn [x] (not (empty? (filter #(= % childName) (get x 2))))) tree))
)

(defn find-bottom [tree]
    (first (filter (fn [x] (not (find-parent-of tree (get x 0) ))) tree))
)

(declare get-total-weight)

(defn get-child-weight [tree node]
    (def children (get node 2))
    (def weights (map #(get-total-weight tree %) children))
    (reduce + weights)
)

(defn get-node [tree nodeName]
    (first (filter #(= (get % 0) nodeName) tree))
)

(defn get-total-weight [tree nodeName]
    (def node (get-node tree nodeName))
    (if (empty? (get node 2)) 
        (get node 1) 
        (+ (get node 1) (get-child-weight tree node))
    )
)

(defn find-different-weight [weights]
    (def l (seq weights))
    (def chk (get (first l) 1))
    (def ne (filter #(not= (get % 1) chk) l))
    (if (= (count ne) 1)
        (first ne)
        (first l)
    )
)

(defn get-sibling-weight [tree node]
    (def parent (find-parent-of tree (get node 0)))
    (def children (get parent 2))
    (def otherChild (first (filter #(not= (get % 0) (get node 0)) children)))
    (get-total-weight tree (get otherChild 0))
)

(defn find-unbalance [tree & nodeName]
    (def node 
        (if (empty? nodeName)
            (find-bottom tree)
            (get-node tree nodeName)
        )
    )
    (def children (get node 2))
    (def weights (into {} (map (fn [x] [x (get-total-weight tree x)]) children)))
    (println (find-different-weight weights))
    (if (every? #{(first weights)} weights)
        (get-sibling-weight tree node)
        (recur tree (find-different-weight weights))
    )
)
